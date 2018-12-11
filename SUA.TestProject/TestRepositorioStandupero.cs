using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUA.Repositorios;
using SUA.Models;
using System.Net.Http;
using System.Net.Http.Headers;


namespace SUA.TestProject
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
            var node = new UriBuilder("localhost")
            { Port = 9200};
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
        public void SiAgregoUnStanduperoExistenteObtengoUnError()
        {
            var dni = "32576829";
            var standupero = CrearStandupero(dni, "Giulianetti", "Bruno", "Argentina");
            repository.AddStandupero(standupero);
            try
            {
                repository.AddStandupero(standupero);
            }
            catch (Exception ex)
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
                if (item.Nombre == "Bruno")
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
            catch (Exception ex)
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

        public Standupero CrearStandupero(string dni, string apellido, string nombre, string pais)
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
                InstagramUser = "@darioorsi"
            };
        }

        [TestMethod]
        public void GetStanduperoFollowers()
        {
            /*
            var users = new List<InstagramUserInfoResponse>();
            var standuperos = new List<string>()
            {
                "joaquin_castellano",
                "chapumartinez",
                "pichipiccirillo",
                "nachitosaralegui",
                "condetodo.yt",
                "ezequielcampa",
                "pablitofabregas",
                "fedesimonetti",
                "crococro",
                "fbuenaventura",
                "lucaslezin",
                "soyradagast",
                "lucaslauriente",
                "luchomellera",
                "lendrogh",
                "mikechouhy",
                "darioorsi",
                "fedecyrulnik",
                "juampicarbonetti",
                "miguegrana",
                "javichosoria",
                "standupargentina",
                "jorgecremades",
                "pipabarbato",
                "nicolasdetracy",
                "lailaroth",
                "dieguitomaggio",
                "fermetilli",
                "juampigon",
                "martin_pugliese",
                "gregorossello",
                "gonzovizan",
                "magalitajes",
                "pablopicotto",
                "acajuanbarraza",
                "angiesammartino",
                "molinerd",
                "sebadibujando",
                "fersanjiao",
                "brullansky",
                "martarresok_",
                "virsammartino",
                "male",
                "malepichot",
            };
            foreach (var item in standuperos)
            {
                users.Add(repository.GeStanduperoInstagramUserInfo(item));
            }
            */
        }

        [TestMethod]
        public void NicolasDeTracyInitialize()
        {
            /*
            //legacy
            int[] followersLegacy = new int[] { 36209, 36628, 36890, 37270, 37582, 37837, 38025, 38091, 38676, 39026, 39684, 40119, 41274, 41438, 42662, 42899, 43155, 43209, 43654, 43955, 44062, 44679, 45020, 45251, 45426, 45497, 45644, 45820, 46072, 46287, 46467, 46664, 46868, 46914, 47060, 47294, 47523, 47695, 47923, 48083, 48304, 48569, 48714, 48940, 49220, 49562, 49874, 50019, 50201, 50398, 50591, 50869, 51073, 51315, 51575, 51997, 52574, 53144, 53566, 54133, 54684, 55148, 55530, 55992, 56334, 56712, 56999, 57382, 57767, 58089, 58534, 58867, 59154, 59631, 59963, 60450, 60866, 61646, 62071, 62339, 62654, 62983, 63384, 64347, 65194, 66124, 66658, 67224, 67617, 68030, 68424, 68894, 69438, 69970, 70507, 70935, 71266, 71622, 72410, 73057, 73818, 74281, 74650, 74945, 75180, 75421, 76048, 76646, 77363, 77909, 78481, 79073, 79694, 80078, 80534, 81197, 81700, 82048, 82396, 82733, 82991, 83944, 84636, 85189, 85495, 85686, 86741, 87889, 88796, 89508, 90307, 90980, 91509, 92324, 93062, 93796, 94894, 95432, 95641, 96734, 97902, 99886, 102082, 103271, 104410, 105330, 106076, 106708, 107626, 108963, 110412, 111634, 112641, 113602, 114628, 115307, 116599, 117778, 118837, 119673, 120141, 120588, 121159, 121736, 122967, 123944, 124773, 125436, 126088, 127451, 129219, 130771, 131941, 132231, 132976, 134074, 134841, 138643, 140296, 141406, 142178, 143022, 144842, 146367, 148341, 149056, 150322, 150911, 151490, 152102, 154595, 156941, 158416, 159276, 159789, 160219, 160711, 162314, 163195, 164905, 165612, 166004, 166230, 166718, 167245, 168101, 168783, 169132, 169340, 169570, 170120, 171531, 172856, 174981, 175583, 177322, 181550, 183087, 184202, 185137, 186981, 187674, 188515, 189460, 190390, 191050, 191666, 192547, 193009, 193802, 194646, 195101, 196042, 196591, 197548, 198059, 198266, 198479, 198678, 200018, 201067, 201685, 202075, 202372, 202908, 203432, 203870, 204326, 205289, 205773, 206175, 206521, 207065, 207418, 207701, 207971, 208488, 208723, 211065, 212502, 213317, 213980, 214371, 214865, 215770, 217036, 218407, 220808, 222899, 224046, 225043, 225656, 226289, 226894, 228401, 228968, 229442, 229987, 230379, 230865, 231744, 232463, 233647, 234107, 234427, 234757, 235194, 235574, 235866, 236198, 236486, 236630, 236908, 237105, 237235, 237640, 238057, 238521, 238671, 238821, 239075, 239380, 240553, 241113, 241589, 241966, 242278, 242520, 242777, 243303, 244092, 244793, 245207, 245436, 245597, 245782, 246010, 246482, 246736, 246965, 247103, 247266, 247386, 247512, 248510, 248757, 248930, 249000, 249264, 249781, 250538, 250975, 251225, 251497, 251696, 251874, 251993, 252196, 252451, 252770, 252883, 252995, 253329, 253471, 253741, 253885, 254034, 254145, 254242, 254410, 254734, 254905, 255061, 255156, 255275, 255294, 255828, 256464, 257604, 258193, 258668, 258941, 259138, 259162, 259491, 259691, 260187, 260350, 260523, 260911, 261060, 261345, 261499, 261536, 261550, 261715, 261765, 261896, 261976, 262076, 262385, 262468, 262575, 263261, 263688, 264410, 264626, 264856, 265146, 265404, 265508, 265620, 265838, 265945, 266060, 266185, 266270, 266618, 266774, 267088, 267248, 267290, 267437, 267522, 267654, 268025, 268311, 268524, 268705, 268766, 268896, 269093, 269436, 269520, 269830, 269939, 270067, 270175, 270256, 270414, 270523, 270610, 270711, 270860, 271057, 271182, 271498, 271637, 271704, 271680, 271746, 272109, 272658, 273168, 273318, 273435, 273558, 273856, 274033, 277649, 278929, 279493, 279604, 279580, 279733, 279860, 280097, 280202, 280121, 280182, 280256, 280308, 280359, 280516, 280528, 280689, 280674, 280729, 280694, 280720, 280746, 280960, 281135, 281192, 281252, 281390, 281443, 289524, 289884, 289975, 290206, 290622, 291316, 292054, 292634, 292866, 293285, 293625, 293885, 294058, 294370, 294662, 294821, 294907, 295152, 295311, 295558, 295731, 296538, 296960, 297143, 297312, 297372, 297623, 297682, 297807, 297891, 297975, 297861, 297849, 297784, 297830, 297966, 297941, 298002, 298120, 298007, 298108, 298297, 298571, 298679, 298740, 306136, 306379, 306397, 306826, 307144, 307397, 307142, 307271, 307389, 307656, 307788, 307815, 308023, 308271, 308564, 308645, 308659, 308770, 308756, 308519, 310362, 311125, 311510, 311770, 311823, 312200, 312273, 312210, 312746, 313684, 313874, 313890, 317656, 320357, 320491, 321159, 321933, 322154, 322120, 322206, 322218, 322286, 322348, 322337, 322238, 322218, 322225, 322205, 322267, 322283, 322198, 321997, 321958, 321820, 321393, 321369, 321294, 321147, 321246, 323888, 323901, 323912, 323955, 323823, 324802, 324765, 324691, 325845, 325433, 325222, 327633, 328290, 328428, 328473, 333666, 333999, 334066, 334189, 334413, 334523, 334677, 335100, 335369, 335444, 335471, 335535, 335535, 335555, 335419, 335439, 335482, 335509, 335554, 335552, 335528, 335382, 335287, 335144, 335819, 336265, 336339, 336249, 336172, 336114, 336062, 336007, 336220, 336308, 336253, 336254, 336210, 336131, 336079, 343452, 343656, 343739, 343679, 343465, 343261, 343062, 343219, 343109, 343052, 342813, 342952, 342712, 342697, 342551, 342500, 342486, 342395, 342700, 343744, 348306, 348816, 348884, 350157, 350314, 350142, 350172, 350569, 350621, 350650, 350589, 350708, 350714, 350796, 350844, 350716, 350593, 350824, 351066, 351434, 351403, 351399, 351375, 351120, 351326, 351385, 351669, 351881, 351961, 351977, 351789, 352009, 352142, 352239, 352291, 352243, 352161, 352246, 352673, 352893, 352969, 353033, 353283, 353340, 353359, 353338, 353277, 353340, 353399, 353497, 353781, 353804, 354103, 354253, 354476, 354648, 354717, 354868, 355002, 355658, 355821, 355824, 355740, 355682, 355580, 355610, 355597, 355592, 355481, 355367, 359582, 359819, 359873, 359847, 359845, 359689, 359698, 359257, 358835, 358636, 358767, 358674, 358473, 358293, 358044, 357948, 357698, 357514, 357584, 358782, 359316, 359472, 359512, 359593, 359518, 359548, 359391, 359258, 359172, 358932, 358697, 358746, 361613, 366318, 367372, 367531, 367559, 367687, 367654, 367650, 367912, 372086, 372756, 372734, 373049};
            var startPoint = new DateTime(2015, 12, 01);
            var legacyGraphic = new Dictionary<DateTime, int>();
            foreach (var item in followersLegacy)
            {
                legacy.Add(startPoint, item);
                startPoint = startPoint.AddDays(1);
            }

            int a = 0;
            a++;
            */
        }
    }
}
