using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUA.Repositorios;
using SUA.Models;

namespace SUA.TestProject
{
    [TestClass]
    public class TestRepositorioProductor
    {
        ESRepositorio repository;
        ESSettings settings;
        string index;


        [TestInitialize]
        public void Setup()
        {
            var node = new UriBuilder("localhost")
            {
                Port = 9200
            };
            settings = new ESSettings(node);
            index = "test_" + ESRepositorio.ContentType.standupero.ToString();
            repository = new ESRepositorio(settings, index);
            repository.CreateIndex();
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
        public void SiAgregoUnProductorExistenteObtengoUnError()
        {
            var dni = "32576829";
            var productor = CrearProductor(dni, "Giulianetti", "Bruno", "Argentina");
            repository.AddProductor(productor);
            try
            {
                repository.AddProductor(productor);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.PRODUCTOR_CREATE_ALREADY_EXISTS_EXCEPTION);
            }

        }
        [TestMethod]
        public void PuedoAgregarUnProductorCorrectamente()
        {
            var dni = "32576829";
            var productor = CrearProductor(dni, "Giulianetti", "Bruno", "Argentina");
            repository.AddProductor(productor);
            var productorObtenido = repository.GetProductorByDni(dni);
            Assert.AreEqual(productor, productorObtenido);
        }
        [TestMethod]
        public void PuedoObtenerTodosLosProductoresCorrectamente()
        {
            var productor = CrearProductor("32576829", "Giulianetti", "Bruno", "Argentina");
            var productor2 = CrearProductor("36621192", "Tuninetti", "Paula", "Cordoba");
            repository.AddProductor(productor);
            repository.AddProductor(productor2);
            var productores = repository.GetProductores();
            foreach (var item in productores)
            {
                if (item.Nombre == "Bruno")
                    Assert.AreEqual(productor, item);
                else if (item.Nombre == "Paula")
                    Assert.AreEqual(productor2, item);
            }
        }
        [TestMethod]
        public void PuedoObtenerUnProductorPorApellidoCorrectamente()
        {
            var productor = CrearProductor("32576829", "Giulianetti", "Bruno", "Argentina");
            repository.AddProductor(productor);
            var productorObtenido = repository.GetProductorByApellido(productor.Apellido);
            Assert.AreEqual(productor, productorObtenido);
        }
        [TestMethod]
        public void SiModificoUnProductorInexistenteObtengoUnError()
        {
            var dni = "32576829";
            var productor = CrearProductor(dni, "Giulianetti", "Bruno", "Argentina");
            try
            {
                repository.UpdateProductor(productor);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.PRODUCTOR_UPDATE_NOT_EXISTS_EXCEPTION);
            }
        }
        [TestMethod]
        public void PuedoModificarUnProductorCorrectamente()
        {
            var dni = "32576829";
            var productor = CrearProductor(dni, "Giulianetti", "Bruno", "Argentina");
            repository.AddProductor(productor);
            productor.Nombre = "Nombre cambiado";
            repository.UpdateProductor(productor);

            var productorObtenido = repository.GetProductorByDni(productor.Dni);
            Assert.AreEqual(productorObtenido, productor);
        }
        [TestMethod]
        public void SiEliminoUnProductorInexistenteObtengoUnError()
        {
            var dni = "32576829";
            var productor = CrearProductor(dni, "Giulianetti", "Bruno", "Argentina");
            repository.CreateIndex();
            try
            {
                repository.DeleteProductor(productor.Dni);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.PRODUCTOR_DELETE_NOT_EXISTS_EXCEPTION);
            }
        }
        [TestMethod]
        public void PuedoEliminarUnProductorCorrectamente()
        {
            var dni = "32576829";
            var productor = CrearProductor(dni, "Giulianetti", "Bruno", "Argentina");
            repository.AddProductor(productor);

            repository.DeleteProductor(productor.Dni);

            var producorObtenido = repository.GetProductorByDni(productor.Dni);
            Assert.AreEqual(producorObtenido, null);
        }

        public Productor CrearProductor(string dni, string apellido, string nombre, string pais)
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
    }
}
