using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUA.Repositorios;
using SUA.Models;
using System.Collections.Generic;
using SUA.Utilities;

namespace SUA.Test
{
    [TestClass]
    public class TestRepositorioShow
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
            index = "test_" + ESRepositorio.ContentType.show.ToString();
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
        public void SiAgregoUnShowExistenteObtengoUnError()
        {
            var show = CrearShow("show");
            repository.AddShow(show);
            try
            {
                repository.AddShow(show);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.SHOW_CREATE_ALREADY_EXISTS_EXCEPTION);
            }

        }

        [TestMethod]
        public void PuedoAgregarUnShowCorrectamente()
        {
            var nombre = "innombrable";
            var show = CrearShow(nombre);
            repository.AddShow(show);
            var showObtenido = repository.GetShowByNombre(nombre);
            Assert.AreEqual(show, showObtenido);
        }

        [TestMethod]
        public void PuedoObtenerTodosLosShowsCorrectamente()
        {
            var show1 = CrearShow("desubicado");
            repository.AddShow(show1);
            var show2 = CrearShow("innombrable");
            repository.AddShow(show2);

            var shows = repository.GetShows();
            foreach (var item in shows)
            {
                if (item.Nombre == "desubicado")
                    Assert.AreEqual(show1, item);
                else if (item.Nombre == "innombrable")
                    Assert.AreEqual(show2, item);
            }
        }

        [TestMethod]
        public void PuedoObtenerUnShowPorNombreCorrectamente()
        {
            var nombre = "desubicado";
            var show = CrearShow(nombre);
            repository.AddShow(show);
            var showObtenido = repository.GetShowByNombre(show.Nombre);
            Assert.AreEqual(show, showObtenido);
        }

        [TestMethod]
        public void SiModificoUnShowInexistenteObtengoUnError()
        {
            var nombre = "show";
            var show = CrearShow(nombre);
            try
            {
                repository.UpdateShow(show);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.SHOW_DELETE_NOT_EXISTS_EXCEPTION);
            }
        }

        [TestMethod]
        public void PuedoModificarUnShowCorrectamente()
        {
            var nombre = "show";
            var show = CrearShow(nombre);
            repository.AddShow(show);
            show.Nombre = "Nombre cambiado";
            repository.UpdateShow(show);

            var showObtenido = repository.GetShowByNombre(show.Nombre);
            Assert.AreEqual(showObtenido, show);
        }

        [TestMethod]
        public void SiEliminoUnShowInexistenteObtengoUnError()
        {
            var nombre = "show";
            var show = CrearShow(nombre);
            repository.CreateIndex();
            try
            {
                repository.GetShowByNombre(show.Nombre);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.SHOW_DELETE_NOT_EXISTS_EXCEPTION);
            }
        }

        [TestMethod]
        public void PuedoEliminarUnShowCorrectamente()
        {
            var nombre = "show";
            var show = CrearShow(nombre);
            repository.AddShow(show);

            repository.DeleteShow(show.UniqueId);

            var showObtenido = repository.GetShowByNombre(show.Nombre);
            Assert.AreEqual(showObtenido, null);
        }

        private Productor CrearProductor(string dni, string apellido, string nombre, string pais)
        {
            return new Productor
            {
                Nombre = nombre,
                Apellido = apellido,
                Direccion = new Ubicacion { Direccion = "Migueletes 680", Localidad = "CABA", Ciudad = "CABA", CodigoPostal = "1426", Provincia = "Buenos Aires", Pais = pais },
                Dni = dni,
                FechaAlta = DateTime.Now,
                FechaNacimiento = new DateTime(1986, 10, 10),
                TransportePropio = "bicicleta",
                DatosBancarios = new DatosBancarios { TipoCuenta = "Caja de Ahorro", Alias = "musica.caoba.jaula", Banco = "BANCO SANTANDER RIO", Cbu = "cbu", CuilCuit = "20-32576829-1", NombreCompleto = "Bruno Nicolas giulianetti" },
                Observaciones = "Ninguna Observacion",
                Celular = "1122526344",
                Email = "bruno.giulianetti@gmail.com"
            };
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
        private Show CrearShow(string nombre)
        {
            return new Show {
                UniqueId = UtilitiesAndStuff.GenerateId(),
                _Show = "Sanata",
                Nombre = nombre,
                Integrantes = new List<Standupero> { CrearStandupero("12345989", "Chouy", "Mike", "Argentina"), CrearStandupero("36621192", "Orsi", "Dario", "Argentina"), },
                Rider = "5 microfonos",
                Camarin = "camarin muy grande",
                Observaciones = "Son graciosos",
                FechaAlta = DateTime.Now,
                Productor = CrearProductor("32576829", "Giulianeetti", "Federico", "Argentina")
            };
        }

    }
}
