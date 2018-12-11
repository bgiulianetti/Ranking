using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUA.Repositorios;
using System.Collections.Generic;
using SUA.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SUA.Test
{
    [TestClass]
    public class TestRepositorioStandupero
    {
        ESRepositorio repository;
        ESSettings settings;
        string index;

        [TestInitialize]
        public void Setup()
        {
            var node = new UriBuilder("localhost");
            node.Port = 9200;
            settings = new ESSettings(node);
            index = "test_" + ESRepositorio.ContentType.standupero.ToString();
            repository = new ESRepositorio(settings, index);
            DeleteIndex();
        }

        [TestCleanup]
        public void CleanUp()
        {
            DeleteIndex();
            repository = null;
        }

        private void DeleteIndex()
        {
            repository.DeleteIndex();
        }

        [TestMethod]
        public void SiAgregoUnStanduperoExistenteObtengoUnError()
        {
            var dni = "32576829";
            var standupero = CrearStandupero(dni, "Giulianetti", "Bruno", "Argentina");
            repository.AddStandupero(standupero);
            try
            {
                repository.AddStandupero(standupero);
            }
            catch(Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.STANDUPERO_CREATE_ALREADY_EXISTS_EXCEPTION);
            }
            
        }

        [TestMethod]
        public void PuedoAgregarUnStanduperoCorrectamente()
        {
            var dni = "32576829";
            var standupero = CrearStandupero(dni, "Giulianetti", "Bruno", "Argentina");
            repository.AddStandupero(standupero);
            var standuperoObtenido = repository.GetStanduperoByDni(dni);
            Assert.AreEqual(standupero, standuperoObtenido);
        }

        [TestMethod]
        public void PuedoObtenerTodosLosStanduperoCorrectamente()
        {
            var standupero = CrearStandupero("32576829", "Giulianetti", "Bruno", "Argentina");
            var standupero2 = CrearStandupero("36621192", "Tuninetti", "Paula", "Cordoba");
            repository.AddStandupero(standupero);
            repository.AddStandupero(standupero2);
            var standuperos = repository.GetStanduperos();
            foreach (var item in standuperos)
            {
                if(item.Nombre == "Bruno")
                    Assert.AreEqual(standupero, item);
                else if (item.Nombre == "Paula")
                    Assert.AreEqual(standupero2, item);
            } 
        }

        [TestMethod]
        public void PuedoObtenerUnStanduperoPorApellidoCorrectamente()
        {
            var standupero = CrearStandupero("32576829", "Giulianetti", "Bruno", "Argentina");
            repository.AddStandupero(standupero);
            var standuperoObtenido = repository.GetStanduperoByApellido(standupero.Apellido);
            Assert.AreEqual(standupero, standuperoObtenido);
        }

        [TestMethod]
        public void SiModificoUnStanduperoInexistenteObtengoUnError()
        {
            var dni = "32576829";
            var standupero = CrearStandupero(dni, "Giulianetti", "Bruno", "Argentina");
            try
            {
                repository.UpdateStandupero(standupero);
            }
            catch(Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.STANDUPERO_UPDATE_NOT_EXISTS_EXCEPTION);
            }
        }

        [TestMethod]
        public void PuedoModificarUnStanduperoCorrectamente()
        {
            var dni = "32576829";
            var standupero = CrearStandupero(dni, "Giulianetti", "Bruno", "Argentina");
            repository.AddStandupero(standupero);
            standupero.Nombre = "Nombre cambiado";
            repository.UpdateStandupero(standupero);

            var standuperoObtenido = repository.GetStanduperoByDni(standupero.Dni);
            Assert.AreEqual(standuperoObtenido, standupero);
        }

        [TestMethod]
        public void SiEliminoUnStanduperoInexistenteObtengoUnError()
        {
            var dni = "32576829";
            var standupero = CrearStandupero(dni, "Giulianetti", "Bruno", "Argentina");
            repository.CreateIndex();
            try
            {
                repository.DeleteStandupero(standupero.Dni);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.STANDUPERO_DELETE_NOT_EXISTS_EXCEPTION);
            }
        }

        [TestMethod]
        public void PuedoEliminarUnStanduperoCorrectamente()
        {
            var dni = "32576829";
            var standupero = CrearStandupero(dni, "Giulianetti", "Bruno", "Argentina");
            repository.AddStandupero(standupero);

            repository.DeleteStandupero(standupero.Dni);

            var standuperoObtenido = repository.GetStanduperoByDni(standupero.Dni);
            Assert.AreEqual(standuperoObtenido, null);
        }

        private Standupero CrearStandupero(string dni, string apellido, string nombre, string pais)
        {
            return new Standupero
            {
                Nombre = nombre,
                Apellido = apellido,
                Direccion = new Ubicacion { Direccion = "Migueletes 680", Localidad = "CABA", Ciudad = "CABA", CodigoPostal = "1426", Provincia = "Buenos Aires", Pais = pais },
                Dni = dni,
                FechaAlta = DateTime.Now,
                FechaNacimiento = new DateTime(1986, 10, 10),
                TransportePropio = "bicicleta",
                Foto = "url de una foto",
                DatosBancarios = new DatosBancarios { TipoCuenta = "Caja de Ahorro", Alias = "musica.caoba.jaula", Banco = "BANCO SANTANDER RIO", Cbu = "cbu", CuilCuit = "20-32576829-1", NombreCompleto = "Bruno Nicolas giulianetti" },
                Observaciones = "Ninguna Observacion",
                Celular = "1122526344",
                Email = "bruno.giulianetti@gmail.com",
                InstagramUser = "@bgiulianetti"
            };
        }

        [TestMethod]
        public void GetStanduperoFollowers()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://www.instagram.com/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var request = "nicolasdetracy";
            var response = Client.GetAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseJson = response.Content.ReadAsStringAsync().Result;
            }
        }

    }
}
