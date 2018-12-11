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
    public class TestRepositorioSala
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
            index = "test_" + ESRepositorio.ContentType.sala.ToString();
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
        public void SiAgregoUnaSalaExistenteObtengoUnError()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            var sala = CrearSala(id);
            repository.AddSala(sala);
            try
            {
                repository.AddSala(sala);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.SALA_CREATE_ALREADY_EXISTS_EXCEPTION);
            }

        }

        [TestMethod]
        public void PuedoAgregarUnaSalaCorrectamente()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            var sala = CrearSala(id);
            repository.AddSala(sala);
            var salaObtenida = repository.GetSalaById(id);
            Assert.AreEqual(sala, salaObtenida);
        }

        [TestMethod]
        public void PuedoObtenerTodasLasSalasCorrectamente()
        {
            var id1 = UtilitiesAndStuff.GenerateUniqueId();
            var sala1 = CrearSala(id1);
            repository.AddSala(sala1);

            //genero retraso
            int i = 0; i++; i.ToString();

            var id2 = UtilitiesAndStuff.GenerateUniqueId().Replace("2","f");
            var sala2 = CrearSala(id2);
            sala2.Nombre = "Teatro Maipo";
            sala2.Direccion.Provincia = "Córdoba";
            repository.AddSala(sala2);

            var salas = repository.GetSalas();
            foreach (var item in salas)
            {
                if (item.UniqueId == id1)
                    Assert.AreEqual(sala1, item);
                else if (item.UniqueId == id2)
                    Assert.AreEqual(sala2, item);
            }
        }

        [TestMethod]
        public void PuedoObtenerUnaSalaPorProvinciaCorrectamente()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            var sala = CrearSala(id);
            repository.AddSala(sala);
            var salaObtenida = repository.GetSalaByProvincia(sala.Direccion.Provincia);
            Assert.AreEqual(sala, salaObtenida);
        }

        [TestMethod]
        public void SiModificoUnaSalaInexistenteObtengoUnError()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            var sala = CrearSala(id);
            try
            {
                repository.UpdateSala(sala);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.SALA_UPDATE_NOT_EXISTS_EXCEPTION);
            }
        }

        [TestMethod]
        public void PuedoModificarUnaSalaCorrectamente()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            var sala = CrearSala(id);
            repository.AddSala(sala);
            sala.Nombre = "Nombre cambiado";
            repository.UpdateSala(sala);

            var salaObtenida = repository.GetSalaById(sala.UniqueId);
            Assert.AreEqual(salaObtenida, sala);
        }

        [TestMethod]
        public void SiEliminoUnaSalaInexistenteObtengoUnError()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            var sala = CrearSala(id);
            repository.CreateIndex();
            try
            {
                repository.DeleteSala(sala.UniqueId);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, ESRepositorio.SALA_DELETE_NOT_EXISTS_EXCEPTION);
            }
        }

        [TestMethod]
        public void PuedoEliminarUnStanduperoCorrectamente()
        {
            var id = UtilitiesAndStuff.GenerateUniqueId();
            var sala = CrearSala(id);
            repository.AddSala(sala);

            repository.DeleteSala(sala.UniqueId);

            var salaObtenida = repository.GetSalaById(sala.UniqueId);
            Assert.AreEqual(salaObtenida, null);
        }

        public Sala CrearSala(string id)
        {
            return new Sala
            {
                AclaracionesSala = "Aclaraciones sala",
                Acomodadores = "Si. acomodadores",
                Administrador = "Si. Admin perez",
                Argentores = "Si. 35%",
                Arreglo = "fifty fifty",
                ArregloEconomico = "arreglo economico bueno",
                Boleteria = new Boleteria { Descripcion = "Boleteria linda", Horario = "de 11 a 20", Telefono = "0800-bolateria"},
                Boletero = "Boletero de los buenos",
                Camarin = new Camarin { Descripcion = "camarin sucio", Toilet = "si, pero no tiene ducha"},
                Capacidad = 1000,
                Carteleria = new Carteleria { Cantidad = 10, Descripcion = "pasacalles", Precio = 256},
                ComoPagan = "a los 60 dias",
                Direccion = new Ubicacion { Direccion = "Migueletes 680", Localidad = "CABA", Ciudad = "CABA", CodigoPostal = "1426", Provincia = "Buenos Aires", Pais = "Argentina" },
                Email = "email@dominio.com",
                FechaAlta = DateTime.Now,
                Horario = "de 9 a 18hs",
                ImpuestosYGastos = new List<string> { "ingresos brutos", "iva", "sadaic"},
                Luces = "Si. pares en todo el frente",
                Nombre = "Teatro colon",
                Pantalla = "Pantalla gigante de 345 pulgadas",
                Prensa = new Contacto { Descripcion = "descripcion de la prensa", Email = "prensa@teatrocom", Nombre = "juan carlos prensa", Telefono = "0800-prensa"},
                Proyector = "No. Traigan el suyo",
                PruebaSonidoEnsayo = new PruebaSonidoEnsayo { Descripcion = "descripcion de la prueba de sonido", Horario = "de 19 a 22hs"},
                Sonido = "sonido array. buffer 24 pulgadas",
                TecnicoLuces = new Contacto { Descripcion = "descripcion de tecnico de luces", Email = "tecnico.luces@teatro.com", Nombre = "Luces roberto", Telefono = "0800-luces"},
                TecnicoSonido = new Contacto { Descripcion = "descripcion de tecnico de sonido", Email = "tecnico.sonido@teatro.com", Nombre = "Sonido roberto", Telefono = "0800-sonido" },
                Telefono = "0800-teatro",
                TicketeraOnline = "Si, pero es una garcha",
                UniqueId = id,
                WhatsAppPersonal = "personal whatsapp",
                RepeticionEnDias = 60
            };
        }

    }
}
