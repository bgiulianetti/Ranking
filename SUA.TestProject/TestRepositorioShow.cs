using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUA.Repositorios;
using SUA.Models;
using SUA.Utilities;

namespace SUA.TestProject
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
            var node = new UriBuilder("localhost")
            { Port = 9200 };
            settings = new ESSettings(node);
            index = "test_" + ESRepositorio.ContentType.show.ToString();
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
                Assert.AreEqual(ex.Message, ESRepositorio.SHOW_UPDATE_NOT_EXISTS_EXCEPTION);
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
                repository.DeleteShow(show.Nombre);
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
  
        public Show CrearShow(string nombre)
        {
            var show = new Show
            {
                _Show = "Sanata",
                Nombre = nombre,
                Integrantes = new List<Standupero> { new TestRepositorioStandupero().CrearStandupero("12345989", "Chouy", "Mike", "Argentina"),
                                                     new TestRepositorioStandupero().CrearStandupero("36621192", "Orsi", "Dario", "Argentina"), },
                Rider = "5 microfonos",
                Camarin = "camarin muy grande",
                Observaciones = "Son graciosos",
                Productor = new TestRepositorioProductor().CrearProductor("32576829", "Giulianeetti", "Federico", "Argentina")
            };
            show.SetIdAndFechaAlta();
            return show;
        }
    }
}
