using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUA.Repositorios;
using SUA.Utilities;
using SUA.Models;

namespace SUA.TestProject
{
    [TestClass]
    public class TestRepositorioFecha
    {
        ESRepositorio repository;
        ESSettings settings;
        string index;

        [TestInitialize]
        public void Setup()
        {
            var node = new UriBuilder("localhost")
            { Port = 9200 };
            settings = new ESSettings(node);
            index = "test_" + ESRepositorio.ContentType.fecha.ToString();
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
        public void SiAgregoUnaFechaExistenteObtengoUnError()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();

            //codigo para generar retraso
            int a = 0; a++;


            var idSala = UtilitiesAndStuff.GenerateUniqueId();
            var fecha = CrearFecha(id, DateTime.Now, idSala);
            repository.AddFecha(fecha);
            try
            {
                repository.AddFecha(fecha);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.FECHA_CREATE_ALREADY_EXISTS_EXCEPTION);
            }

        }

        [TestMethod]
        public void PuedoAgregarUnaFechaCorrectamente()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();

            //codigo para generar retraso
            int a = 0; a++;

            var idSala = UtilitiesAndStuff.GenerateUniqueId();
            var fecha = CrearFecha(id, DateTime.Now, idSala);
            repository.AddFecha(fecha);
            var fechaObtenida = repository.GetFechaById(fecha.UniqueId);
            Assert.AreEqual(fecha, fechaObtenida);
        }

        [TestMethod]
        public void PuedoObtenerTodosLosShowsCorrectamente()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            //codigo para generar retraso
            int a = 0; a++;

            var idSala = UtilitiesAndStuff.GenerateUniqueId();
            var fecha = CrearFecha(id, DateTime.Now, idSala);
            repository.AddFecha(fecha);

            var id2 = UtilitiesAndStuff.GenerateUniqueId();
            //codigo para generar retraso
            int b = 0; b++;

            var idSala2 = UtilitiesAndStuff.GenerateUniqueId();
            var fecha2 = CrearFecha(id2, DateTime.Now, idSala2);
            repository.AddFecha(fecha2);

            var fechas = repository.GetFechas();
            foreach (var item in fechas)
            {
                if (item.UniqueId == id)
                    Assert.AreEqual(fecha, item);
                else if (item.UniqueId == id2)
                    Assert.AreEqual(fecha2, item);
            }
        }

        [TestMethod]
        public void PuedoObtenerFechasPorShowCorrectamente()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            //codigo para generar retraso
            int a = 0; a++;

            var idSala = UtilitiesAndStuff.GenerateUniqueId();
            var fecha = CrearFecha(id, DateTime.Now, idSala);
            repository.AddFecha(fecha);

            var fechasObtenidas = repository.GetFechasByShow(fecha.Show.Nombre);
            foreach (var item in fechasObtenidas)
            {
                Assert.AreEqual(fecha, item);
            }

        }

        [TestMethod]
        public void PuedoObtenerFechasPorSalaCorrectamente()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            //codigo para generar retraso
            int a = 0; a++;

            var idSala = UtilitiesAndStuff.GenerateUniqueId();
            var fecha = CrearFecha(id, DateTime.Now, idSala);
            repository.AddFecha(fecha);

            var fechasObtenidas = repository.GetFechasBySala(fecha.Sala.Nombre);
            foreach (var item in fechasObtenidas)
            {
                Assert.AreEqual(fecha, item);
            }

        }

        [TestMethod]
        public void PuedoObtenerUnaFechaPorProvinciaCorrectamente()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            //codigo para generar retraso
            int a = 0; a++; a.ToString();

            var idSala = UtilitiesAndStuff.GenerateUniqueId();
            var fecha = CrearFecha(id, DateTime.Now, idSala);
            repository.AddFecha(fecha);

            var fechasObtenidas = repository.GetFechasByProvincia(fecha.Sala.Direccion.Provincia);
            foreach (var item in fechasObtenidas)
            {
                Assert.AreEqual(fecha, item);
            }
        }

        [TestMethod]
        public void PuedoObtenerUnaFechaPorSalaYHorarioCorrectamente()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            //codigo para generar retraso
            int a = 0; a++;

            var idSala = UtilitiesAndStuff.GenerateUniqueId();
            var fecha = CrearFecha(id, DateTime.Now, idSala);
            repository.AddFecha(fecha);

            var fechaObtenida = repository.GetFechaBySalaAndFechaAndHorario(fecha.Sala.UniqueId, fecha.FechaHorario);
            Assert.AreEqual(fecha, fechaObtenida);
        }

        [TestMethod]
        public void SiModificoUnaFechaInexistenteObtengoUnError()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            //codigo para generar retraso
            int a = 0; a++;

            var idSala = UtilitiesAndStuff.GenerateUniqueId();
            var fecha = CrearFecha(id, DateTime.Now, idSala);
            try
            {
                repository.UpdateFecha(fecha);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.FECHA_UPDATE_NOT_EXISTS_EXCEPTION);
            }
        }

        [TestMethod]
        public void PuedoModificarUnaFechaCorrectamente()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            //codigo para generar retraso
            int a = 0; a++;

            var idSala = UtilitiesAndStuff.GenerateUniqueId();
            var fecha = CrearFecha(id, DateTime.Now, idSala);
            repository.AddFecha(fecha);

            fecha.FechaHorario = new DateTime(2018, 10, 10, 22, 00, 00);
            repository.UpdateFecha(fecha);

            var fechaObtenida = repository.GetFechaById(fecha.UniqueId);
            Assert.AreEqual(fechaObtenida, fecha);
        }

        [TestMethod]
        public void SiEliminoUnaFechaInexistenteObtengoUnError()
        {
            
            var fecha = CrearFecha(UtilitiesAndStuff.GenerateUniqueId(), DateTime.Now, UtilitiesAndStuff.GenerateUniqueId());
            repository.CreateIndex();
            try
            {
                repository.DeleteFecha(fecha.UniqueId);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.FECHA_DELETE_NOT_EXISTS_EXCEPTION);
            }
        }

        [TestMethod]
        public void PuedoEliminarUnaFechaCorrectamente()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            //codigo para generar retraso
            int a = 0; a++;

            var idSala = UtilitiesAndStuff.GenerateUniqueId();
            var fecha = CrearFecha(id, DateTime.Now, idSala);
            repository.AddFecha(fecha);

            repository.DeleteFecha(fecha.UniqueId);

            var fechaObtenida = repository.GetFechaById(fecha.UniqueId);
            Assert.AreEqual(fechaObtenida, null);
        }

        [TestMethod]
        public void CheckVencimiento()
        {
            //var fecha = new DateTime(2018, 08, 12);
            //var diasVencido = UtilitiesAndStuff.CalcularVencimiento(fecha, DateTime.Now, 60);
        }


        private Fecha CrearFecha(string id, DateTime fechaYhorario, string idSala)
        {
            return new Fecha
            {
                Borederaux = null,
                FechaHorario = fechaYhorario,
                Productor = new TestRepositorioProductor().CrearProductor("32576829", "Giulianetti", "0800-fede", "Argentina"),
                Sala = new TestRepositorioSala().CrearSala(idSala),
                Show = new TestRepositorioShow().CrearShow("Sanata"),
                UniqueId = id
            };
        }

    }
}
