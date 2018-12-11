using SUA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SUA.Utilities
{
    public static class UtilitiesAndStuff
    {
        public static List<SelectListItem> GetEstados()
        {
            var estados = new List<string>
            {
                "si",
                "no"
            };
            return ConverListToSelectListItem(estados);
        }

        public static List<SelectListItem> GetPermisos()
        {
            var permisos = new List<string>
            {
                "Prohibido",
                "Lectura",
                "Escritura"
            };
            return ConverListToSelectListItem(permisos);
        }

        public static List<SelectListItem> GetProvincias()
        {
            var provincias = new List<string>
            {
                "Buenos Aires",
                "Catamarca",
                "Chaco",
                "Chubut",
                "Cordoba",
                "Corrientes",
                "Entre Ríos",
                "Formosa",
                "Jujuy",
                "La Pampa",
                "La Rioja",
                "Mendoza",
                "Misiones",
                "Montevideo",
                "Neuquen",
                "Río Negro",
                "Salta",
                "San Juan",
                "San Luis",
                "Santa Cruz",
                "Santa Fe",
                "Santiago del Estero",
                "Tierra del Fuego",
                "Tucumán"
            };
            return ConverProvinciasToSelectListItem(provincias);
        }

        public static List<SelectListItem> GetPaises()
        {
            var paises = new List<string>
            {
                "Argentina",
                "Bolivia​",
                "Chile",
                "Colombia",
                "Ecuador",
                "España",
                "Perú",
                "Uruguay",
                "Venezuela​"
            };
            return ConverListToSelectListItem(paises);
        }

        public static List<SelectListItem> GetBancos()
        {
            var bancos = new List<string> {
                "BANCO GALICIA",
                "BANCO DE LA NACION ARGENTINA",
                "BANCO DE LA PROVINCIA DE BUENOS AIRES",
                "CITIBANK",
                "BBVA BANCO FRANCES",
                "BANCO SUPERVIELLE",
                "BANCO DE LA CIUDAD DE BUENOS AIRES",
                "BANCO PATAGONIA",
                "BANCO HIPOTECARIO",
                "BANCO DE SAN JUAN",
                "BANCO DEL TUCUMAN",
                "BANCO MUNICIPAL DE ROSARIO",
                "BANCO SANTANDER RIO",
                "HSBC BANK ARGENTINA",
                "JPMORGAN CHASE BANK",
                "BANCO CREDICOOP",
                "BANCO MARIVA",
                "BANCO ITAU ARGENTINA",
                "BANCO MACRO",
                "BANCO COMAFI",
                "BANCO FINANSUR",
                "BANCO COLUMBIA"
            };

            bancos = bancos.OrderBy(q => q).ToList();
            return ConverListToSelectListItem(bancos);
        }

        public static List<SelectListItem> ConverListToSelectListItem(List<string> list)
        {
            var selectList = new List<SelectListItem>();
            foreach (var item in list)
                selectList.Add(new SelectListItem { Text = item.Replace(" ", ""), Value = item });

            return selectList;
        }

        public static List<SelectListItem> ConverProvinciasToSelectListItem(List<string> list)
        {
            var selectList = new List<SelectListItem>();
            foreach (var item in list)
                selectList.Add(new SelectListItem { Text = item.Replace(" ", "@"), Value = item });

            return selectList;
        }

        public static string GenerateUniqueId()
        {
            return DateTime.Now.ToString("yyyyMMddHHssfff");
        }

        public static List<SelectListItem> GetImpuestos()
        {
            var impuestos = new List<string>
            {
                "AADET",
                "AADICAPIF",
                "Agadu",
                "Alquiler Teatro",
                "Argentores",
                "COFONTE",
                "Comision por Tarjeta​",
                "IMM",
                "Impuesto Municipal",
                "Luces y Sonido",
                "Otro",
                "SADAIC",
                "Ticketing"
            };
            return ConverListToSelectListItem(impuestos);
        }

        public static List<SelectListItem> GetEntradas()
        {
            var entradas = new List<string>
            {
                "2x1",
                "Aplicacion",
                "Boleteria",
                "Entradas Teatro 1",
                "Entradas Teatro 2",
                "Entradas Teatro 3",
                "Entradas Teatro 4",
                "Entradas Teatro 5",
                "Entradas Teatro 6",
                "Entradas Ticketera 1",
                "Entradas Ticketera 2",
                "Entradas Ticketera 3",
                "Entradas Ticketera 4",
                "Entradas Ticketera 5",
                "Entradas Ticketera 6",
                "Free",
                "Invitaciones",
                "Invitaciones 2",
                "Menor",
                "Otras",
                "Wix",
                "Wix 50% off",
                "Wix 25% off"

            };
            return ConverListToSelectListItem(entradas);
        }

        public static List<SelectListItem> GetGastosCompany()
        {
            var gastos = new List<string>
            {
                "Ads Facebook",
                "Buque",
                "Camarin",
                "Cartel Puerta",
                "Carteles Calle",
                "Combustible",
                "Direccion",
                "Estacionamiento",
                "Hospedaje",
                "Otro",
                "Pasajes avion",
                "Peajes",
                "Propina",
                "Propina Sonido",
                "Publicidad",
                "Sonido",
                "Taxis"
            };
            return ConverListToSelectListItem(gastos);
        }

        public static int CalcularRepeticion(DateTime fechaUltimoShow, DateTime fechaProximoShow, int repeticionEnDias)
        {
            var FechaRepeticion = fechaUltimoShow.AddDays(repeticionEnDias);
            var distanciaEnDias = (fechaProximoShow - FechaRepeticion).TotalDays;
            var distanciaPorcentaje = distanciaEnDias * 100 / repeticionEnDias;
            return Convert.ToInt32(distanciaPorcentaje);
        }

        public static List<SelectListItem> GetColores()
        {
            var colores = new List<string>
            {
                "black",
                "blue",
                "brown",
                "gray",
                "green",
                "lightblue",
                "lightgray",
                "orange",
                "pink",
                "purple",
                "white",
                "yellow"
            };
            return ConverListToSelectListItem(colores);
        }

        public static List<Ciudad> GetCiudades()
        {
            var ciudades = new List<Ciudad>();
            ciudades.Add(new Ciudad("Avellaneda", "Buenos Aires", "Argentina", "342677"));
            ciudades.Add(new Ciudad("Azul", "Buenos Aires", "Argentina", "53054"));
            ciudades.Add(new Ciudad("Bahia Blanca", "Buenos Aires", "Argentina", "299101"));
            ciudades.Add(new Ciudad("Banda del Rio Sali", "Tucuman", "Argentina", "64591"));
            ciudades.Add(new Ciudad("Banfield", "Buenos Aires", "Argentina", "223898"));
            ciudades.Add(new Ciudad("Barranqueras", "Chaco", "Argentina", "50738"));
            ciudades.Add(new Ciudad("Beccar", "Buenos Aires", "Argentina", "58811"));
            ciudades.Add(new Ciudad("Belen de Escobar", "Buenos Aires", "Argentina", "55054"));
            ciudades.Add(new Ciudad("Bella Vista", "Buenos Aires", "Argentina", "67936"));
            ciudades.Add(new Ciudad("Berazategui", "Buenos Aires", "Argentina", "167555"));
            ciudades.Add(new Ciudad("Berisso", "Buenos Aires", "Argentina", "54406"));
            ciudades.Add(new Ciudad("Bernal", "Buenos Aires", "Argentina", "109914"));
            ciudades.Add(new Ciudad("Bosques", "Buenos Aires", "Argentina", "51663"));
            ciudades.Add(new Ciudad("Boulogne Sur Mer", "Buenos Aires", "Argentina", "73496"));
            ciudades.Add(new Ciudad("Burzaco", "Buenos Aires", "Argentina", "86113"));
            ciudades.Add(new Ciudad("Campana", "Buenos Aires", "Argentina", "94461"));
            ciudades.Add(new Ciudad("Capital Federal", "Buenos Aires", "Argentina", "2890151"));
            ciudades.Add(new Ciudad("Caseros", "Buenos Aires", "Argentina", "95785"));
            ciudades.Add(new Ciudad("Castelar", "Buenos Aires", "Argentina", "104019"));
            ciudades.Add(new Ciudad("Chimbas", "San Juan", "Argentina", "73210"));
            ciudades.Add(new Ciudad("Chivilcoy", "Buenos Aires", "Argentina", "87510"));
            ciudades.Add(new Ciudad("Cipolletti", "Rio Negro", "Argentina", "66472"));
            ciudades.Add(new Ciudad("Ciudad de Corrientes", "Corrientes", "Argentina", "314546"));
            ciudades.Add(new Ciudad("Ciudad de Formosa", "Formosa", "Argentina", "222226"));
            ciudades.Add(new Ciudad("Ciudad de La Rioja", "La Rioja", "Argentina", "178872"));
            ciudades.Add(new Ciudad("Ciudad de Mendoza", "Mendoza", "Argentina", "110993"));
            ciudades.Add(new Ciudad("Ciudad de Neuquen", "Neuquen", "Argentina", "201868"));
            ciudades.Add(new Ciudad("Ciudad de Rio Cuarto", "Cordoba", "Argentina", "157010"));
            ciudades.Add(new Ciudad("Ciudad de Salta", "Salta", "Argentina", "535303"));
            ciudades.Add(new Ciudad("Ciudad de San Juan", "San Juan", "Argentina", "112778"));
            ciudades.Add(new Ciudad("Ciudad de San Luis", "San Luis", "Argentina", "250957"));
            ciudades.Add(new Ciudad("Ciudad de Santa Fe", "Santa Fe", "Argentina", "500000"));
            ciudades.Add(new Ciudad("Ciudad de Santiago del Estero", "Santiago del Estero", "Argentina", "230614"));
            ciudades.Add(new Ciudad("Ciudad Evita", "Buenos Aires", "Argentina", "68650"));
            ciudades.Add(new Ciudad("Ciudad Jardin El Libertador", "Buenos Aires", "Argentina", "61780"));
            ciudades.Add(new Ciudad("Ciudadela", "Buenos Aires", "Argentina", "73155"));
            ciudades.Add(new Ciudad("Clorinda", "Formosa", "Argentina", "46884"));
            ciudades.Add(new Ciudad("Comodoro Rivadavia", "Chubut", "Argentina", "212410"));
            ciudades.Add(new Ciudad("Concepcion", "Tucuman", "Argentina", "46194"));
            ciudades.Add(new Ciudad("Concepcion del Uruguay", "Entre Rios", "Argentina", "64538"));
            ciudades.Add(new Ciudad("Concordia", "Entre Rios", "Argentina", "147046"));
            ciudades.Add(new Ciudad("Cordoba", "Cordoba", "Argentina", "1429604"));
            ciudades.Add(new Ciudad("Don Torcuato", "Buenos Aires", "Argentina", "64867"));
            ciudades.Add(new Ciudad("El Jagüel", "Buenos Aires", "Argentina", "48781"));
            ciudades.Add(new Ciudad("El Palomar", "Buenos Aires", "Argentina", "57146"));
            ciudades.Add(new Ciudad("Eldorado", "Misiones", "Argentina", "47794"));
            ciudades.Add(new Ciudad("Esperanza", "Santa Fe", "Argentina", "42082"));
            ciudades.Add(new Ciudad("Ezeiza", "Buenos Aires", "Argentina", "93247"));
            ciudades.Add(new Ciudad("Ezpeleta", "Buenos Aires", "Argentina", "72557"));
            ciudades.Add(new Ciudad("Florencio Varela", "Buenos Aires", "Argentina", "120678"));
            ciudades.Add(new Ciudad("Florida", "Buenos Aires", "Argentina", "75891"));
            ciudades.Add(new Ciudad("General Pico", "La Pampa", "Argentina", "52414"));
            ciudades.Add(new Ciudad("General Roca", "Rio Negro", "Argentina", "69602"));
            ciudades.Add(new Ciudad("General Rodriguez", "Buenos Aires", "Argentina", "63317"));
            ciudades.Add(new Ciudad("Glew", "Buenos Aires", "Argentina", "57878"));
            ciudades.Add(new Ciudad("Gobernador Julio A Costa", "Buenos Aires", "Argentina", "49291"));
            ciudades.Add(new Ciudad("Godoy Cruz", "Mendoza", "Argentina", "182563"));
            ciudades.Add(new Ciudad("Gonzalez Catan", "Buenos Aires", "Argentina", "165206"));
            ciudades.Add(new Ciudad("Goya", "Corrientes", "Argentina", "66462"));
            ciudades.Add(new Ciudad("Grand Bourg", "Buenos Aires", "Argentina", "85159"));
            ciudades.Add(new Ciudad("Gregorio de Laferrere", "Buenos Aires", "Argentina", "175670"));
            ciudades.Add(new Ciudad("Gualeguaychu", "Entre Rios", "Argentina", "74681"));
            ciudades.Add(new Ciudad("Guaymallen", "Mendoza", "Argentina", "223365"));
            ciudades.Add(new Ciudad("Isidro Casanova", "Buenos Aires", "Argentina", "136091"));
            ciudades.Add(new Ciudad("Ituzaingo", "Buenos Aires", "Argentina", "126631"));
            ciudades.Add(new Ciudad("Jose C. Paz", "Buenos Aires", "Argentina", "216637"));
            ciudades.Add(new Ciudad("Junin", "Buenos Aires", "Argentina", "87509"));
            ciudades.Add(new Ciudad("La Banda", "Santiago del Estero", "Argentina", "95142"));
            ciudades.Add(new Ciudad("La Plata", "Buenos Aires", "Argentina", "699523"));
            ciudades.Add(new Ciudad("La Tablada", "Buenos Aires", "Argentina", "80389"));
            ciudades.Add(new Ciudad("Lanus", "Buenos Aires", "Argentina", "459263"));
            ciudades.Add(new Ciudad("Las Heras", "Mendoza", "Argentina", "182563"));
            ciudades.Add(new Ciudad("Libertad", "Buenos Aires", "Argentina", "100476"));
            ciudades.Add(new Ciudad("Lomas de Zamora", "Buenos Aires", "Argentina", "111897"));
            ciudades.Add(new Ciudad("Lomas del Mirador", "Buenos Aires", "Argentina", "52971"));
            ciudades.Add(new Ciudad("Longchamps", "Buenos Aires", "Argentina", "47622"));
            ciudades.Add(new Ciudad("Los Polvorines", "Buenos Aires", "Argentina", "53354"));
            ciudades.Add(new Ciudad("Lujan", "Buenos Aires", "Argentina", "67266"));
            ciudades.Add(new Ciudad("Lujan de Cuyo", "Mendoza", "Argentina", "73058"));
            ciudades.Add(new Ciudad("Maipu", "Mendoza", "Argentina", "89433"));
            ciudades.Add(new Ciudad("Mar del Plata", "Buenos Aires", "Argentina", "664892"));
            ciudades.Add(new Ciudad("Mariano Acosta", "Buenos Aires", "Argentina", "54081"));
            ciudades.Add(new Ciudad("Martinez", "Buenos Aires", "Argentina", "65859"));
            ciudades.Add(new Ciudad("Mercedes", "Buenos Aires", "Argentina", "51967"));
            ciudades.Add(new Ciudad("Merlo", "Buenos Aires", "Argentina", "244168"));
            ciudades.Add(new Ciudad("Monte Chingolo", "Buenos Aires", "Argentina", "85060"));
            ciudades.Add(new Ciudad("Monte Grande", "Buenos Aires", "Argentina", "110241"));
            ciudades.Add(new Ciudad("Moreno", "Buenos Aires", "Argentina", "149317"));
            ciudades.Add(new Ciudad("Moron", "Buenos Aires", "Argentina", "92725"));
            ciudades.Add(new Ciudad("Necochea", "Buenos Aires", "Argentina", "65459"));
            ciudades.Add(new Ciudad("Obera", "Misiones", "Argentina", "51681"));
            ciudades.Add(new Ciudad("Olavarria", "Buenos Aires", "Argentina", "83738"));
            ciudades.Add(new Ciudad("Olivos", "Buenos Aires", "Argentina", "75527"));
            ciudades.Add(new Ciudad("Palpala", "Jujuy", "Argentina", "45077"));
            ciudades.Add(new Ciudad("Parana", "Entre Rios", "Argentina", "247863"));
            ciudades.Add(new Ciudad("Pergamino", "Buenos Aires", "Argentina", "104985"));
            ciudades.Add(new Ciudad("Pilar", "Buenos Aires", "Argentina", "226517"));
            ciudades.Add(new Ciudad("Posadas", "Misiones", "Argentina", "354719"));
            ciudades.Add(new Ciudad("Presidencia Roque Saenz Peña", "Chaco", "Argentina", "76377"));
            ciudades.Add(new Ciudad("Presidente Peron", "Buenos Aires", "Argentina", "52529"));
            ciudades.Add(new Ciudad("Puerto Madryn", "Chubut", "Argentina", "93995"));
            ciudades.Add(new Ciudad("Punta Alta", "Buenos Aires", "Argentina", "57296"));
            ciudades.Add(new Ciudad("Quilmes", "Buenos Aires", "Argentina", "230810"));
            ciudades.Add(new Ciudad("Rafael Calzada", "Buenos Aires", "Argentina", "56419"));
            ciudades.Add(new Ciudad("Rafael Castillo", "Buenos Aires", "Argentina", "103992"));
            ciudades.Add(new Ciudad("Rafaela", "Santa Fe", "Argentina", "82530"));
            ciudades.Add(new Ciudad("Ramos Mejia", "Buenos Aires", "Argentina", "98547"));
            ciudades.Add(new Ciudad("Rawson", "Chubut", "Argentina", "31787"));
            ciudades.Add(new Ciudad("Reconquista", "Santa Fe", "Argentina", "66187"));
            ciudades.Add(new Ciudad("Remedios de Escalada", "Buenos Aires", "Argentina", "81465"));
            ciudades.Add(new Ciudad("Resistencia", "Chaco", "Argentina", "290723"));
            ciudades.Add(new Ciudad("Rio Gallegos", "Santa Cruz", "Argentina", "95796"));
            ciudades.Add(new Ciudad("Rio Grande", "Tierra del Fuego", "Argentina", "52786"));
            ciudades.Add(new Ciudad("Rivadavia", "San Juan", "Argentina", "75950"));
            ciudades.Add(new Ciudad("Rosario", "Santa Fe", "Argentina", "1400000"));
            ciudades.Add(new Ciudad("San Carlos de Bariloche", "Rio Negro", "Argentina", "113299"));
            ciudades.Add(new Ciudad("San Fernando", "Buenos Aires", "Argentina", "69110"));
            ciudades.Add(new Ciudad("San Fernando del Valle de Catamarca", "Catamarca", "Argentina", "159139"));
            ciudades.Add(new Ciudad("San Francisco", "Cordoba", "Argentina", "58588"));
            ciudades.Add(new Ciudad("San Francisco Solano", "Buenos Aires", "Argentina", "53363"));
            ciudades.Add(new Ciudad("San Isidro", "Buenos Aires", "Argentina", "292878"));
            ciudades.Add(new Ciudad("San Justo", "Buenos Aires", "Argentina", "105274"));
            ciudades.Add(new Ciudad("San Martin", "Mendoza", "Argentina", "49491"));
            ciudades.Add(new Ciudad("San Martin", "Buenos Aires", "Argentina", "49491"));
            ciudades.Add(new Ciudad("San Miguel", "Buenos Aires", "Argentina", "157532"));
            ciudades.Add(new Ciudad("San Miguel de Tucuman", "Tucuman", "Argentina", "694327"));
            ciudades.Add(new Ciudad("San Nicolas de los Arroyos", "Buenos Aires", "Argentina", "125408"));
            ciudades.Add(new Ciudad("San Pedro de Jujuy", "Jujuy", "Argentina", "55084"));
            ciudades.Add(new Ciudad("San Rafael", "Mendoza", "Argentina", "118009"));
            ciudades.Add(new Ciudad("San Ramon de la Nueva Oran", "Salta", "Argentina", "66579"));
            ciudades.Add(new Ciudad("San Salvador de Jujuy", "Jujuy", "Argentina", "231229"));
            ciudades.Add(new Ciudad("Santa Rosa", "La Pampa", "Argentina", "101987"));
            ciudades.Add(new Ciudad("Sarandi", "Buenos Aires", "Argentina", "60725"));
            ciudades.Add(new Ciudad("Tandil", "Buenos Aires", "Argentina", "101010"));
            ciudades.Add(new Ciudad("Tartagal", "Salta", "Argentina", "55508"));
            ciudades.Add(new Ciudad("Temperley", "Buenos Aires", "Argentina", "111660"));
            ciudades.Add(new Ciudad("Tigre", "Buenos Aires", "Argentina", "380709"));
            ciudades.Add(new Ciudad("Trelew", "Chubut", "Argentina", "103656"));
            ciudades.Add(new Ciudad("Tres Arroyos", "Buenos Aires", "Argentina", "45986"));
            ciudades.Add(new Ciudad("Trujui", "Buenos Aires", "Argentina", "94608"));
            ciudades.Add(new Ciudad("Ushuaia", "Tierra del Fuego", "Argentina", "56825"));
            ciudades.Add(new Ciudad("Venado Tuerto", "Santa Fe", "Argentina", "68508"));
            ciudades.Add(new Ciudad("Vicente Lopez", "Buenos Aires", "Argentina", "274082"));
            ciudades.Add(new Ciudad("Viedma", "Rio Negro", "Argentina", "46767"));
            ciudades.Add(new Ciudad("Villa Carlos Paz", "Cordoba", "Argentina", "60900"));
            ciudades.Add(new Ciudad("Villa Centenario", "Buenos Aires", "Argentina", "49737"));
            ciudades.Add(new Ciudad("Villa Dolores", "Cordoba", "Argentina", "43625"));
            ciudades.Add(new Ciudad("Villa Dominico", "Buenos Aires", "Argentina", "58824"));
            ciudades.Add(new Ciudad("Villa Gobernador Galvez", "Santa Fe", "Argentina", "74658"));
            ciudades.Add(new Ciudad("Villa Luzuriaga", "Buenos Aires", "Argentina", "73952"));
            ciudades.Add(new Ciudad("Villa Madero", "Buenos Aires", "Argentina", "75582"));
            ciudades.Add(new Ciudad("Villa Maria", "Cordoba", "Argentina", "72162"));
            ciudades.Add(new Ciudad("Villa Mariano Moreno-El Colmenar", "Tucuman", "Argentina", "48655"));
            ciudades.Add(new Ciudad("Villa Mercedes", "San Luis", "Argentina", "97000"));
            ciudades.Add(new Ciudad("Villa Tesei", "Buenos Aires", "Argentina", "63164"));
            ciudades.Add(new Ciudad("Virrey del Pino", "Buenos Aires", "Argentina", "90383"));
            ciudades.Add(new Ciudad("Wilde", "Buenos Aires", "Argentina", "65881"));
            ciudades.Add(new Ciudad("William Morris", "Buenos Aires", "Argentina", "48916"));
            ciudades.Add(new Ciudad("Yerba Buena/Marcos Paz", "Tucuman", "Argentina", "50057"));
            ciudades.Add(new Ciudad("Zarate", "Buenos Aires", "Argentina", "98522"));
            ciudades.Add(new Ciudad("Cochabamba", "Cochabamba", "Bolivia", "632013"));
            ciudades.Add(new Ciudad("El Alto", "La Paz", "Bolivia", "827239"));
            ciudades.Add(new Ciudad("La Paz", "La Paz", "Bolivia", "757184"));
            ciudades.Add(new Ciudad("Santa Cruz de la Sierra", "Santa Cruz", "Bolivia", "1468700"));
            ciudades.Add(new Ciudad("Brasilia", "Brasilia", "Brasil", "2789761"));
            ciudades.Add(new Ciudad("Rio de Janeiro", "Rio de Janeiro", "Brasil", "6476631"));
            ciudades.Add(new Ciudad("Salvador de Bahia", "Bahia", "Brasil", "2948733"));
            ciudades.Add(new Ciudad("São Paulo", "São Paulo", "Brasil", "12106920"));
            ciudades.Add(new Ciudad("Concepcion", "Biobio", "Chile", "848023"));
            ciudades.Add(new Ciudad("La Serena", "Coquimbo", "Chile", "296253"));
            ciudades.Add(new Ciudad("Santiago", "Metropolitana de Santiago", "Chile", "5631839"));
            ciudades.Add(new Ciudad("Valparaiso", "Valparaiso", "Chile", "824006"));
            ciudades.Add(new Ciudad("Barranquilla", "Atlantico", "Colombia", "1228510"));
            ciudades.Add(new Ciudad("Bogota", "Cundinamarca", "Colombia", "8164178"));
            ciudades.Add(new Ciudad("Cali", "Valle del Cauca", "Colombia", "2408653"));
            ciudades.Add(new Ciudad("Medellin", "Antioquia", "Colombia", "2501470"));
            ciudades.Add(new Ciudad("Cuenca", "Azuay", "Ecuador", "329928"));
            ciudades.Add(new Ciudad("Guayaquil", "Guayas", "Ecuador", "2278691"));
            ciudades.Add(new Ciudad("Quito", "Pichincha", "Ecuador", "1607734"));
            ciudades.Add(new Ciudad("Santo Domingo", "Santo Domingo de los Tsachilas", "Ecuador", "270875"));
            ciudades.Add(new Ciudad("Barcelona", "Cataluña", "España", "1620809"));
            ciudades.Add(new Ciudad("Madrid", "Comunidad de Madrid", "España", "3182981"));
            ciudades.Add(new Ciudad("Sevilla", "Andalucia", "España", "689434"));
            ciudades.Add(new Ciudad("Valencia", "Comunidad Valenciana", "España", "787808"));
            ciudades.Add(new Ciudad("Ciudad de Mexico", "Ciudad de Mexico", "Mexico", "8851080"));
            ciudades.Add(new Ciudad("Ecatepec", "Mexico", "Mexico", "1655015"));
            ciudades.Add(new Ciudad("Guadalajara", "Jalisco", "Mexico", "1495182"));
            ciudades.Add(new Ciudad("Puebla de Zaragoza", "Puebla", "Mexico", "1434062"));
            ciudades.Add(new Ciudad("Asuncion", "Distrito Capital", "Paraguay", "523184"));
            ciudades.Add(new Ciudad("Ciudad del Este", "Alto Parana", "Paraguay", "299255"));
            ciudades.Add(new Ciudad("Luque", "Central", "Paraguay", "272808"));
            ciudades.Add(new Ciudad("San Lorenzo", "Central", "Paraguay", "256008"));
            ciudades.Add(new Ciudad("Arequipa", "Arequipa", "Peru", "1008290"));
            ciudades.Add(new Ciudad("Callao", "Callao", "Peru", "994494"));
            ciudades.Add(new Ciudad("Lima", "Lima", "Peru", "9562280"));
            ciudades.Add(new Ciudad("Trujillo", "La Libertad", "Peru", "919899"));
            ciudades.Add(new Ciudad("Maldonado​", "Maldonado", "Uruguay", "86782"));
            ciudades.Add(new Ciudad("Montevideo", "Montevideo", "Uruguay", "1305082"));
            ciudades.Add(new Ciudad("Paysandu​", "Paysandu", "Uruguay", "90690"));
            ciudades.Add(new Ciudad("Salto", "Salto", "Uruguay", "104028"));
            ciudades.Add(new Ciudad("Chacabuco","Buenos Aires","Argentina","38418"));
            ciudades.Add(new Ciudad("Balcarce","Buenos Aires","Argentina","38376"));
            ciudades.Add(new Ciudad("Cosquin - Santa Maria de Punilla - Bialet Masse","Cordoba","Argentina","37273"));
            ciudades.Add(new Ciudad("Nueve de Julio","Buenos Aires","Argentina","36494"));
            ciudades.Add(new Ciudad("La Falda - Huerta Grande - Valle Hermoso","Cordoba","Argentina","35821"));
            ciudades.Add(new Ciudad("Casilda","Santa Fe","Argentina","35058"));
            ciudades.Add(new Ciudad("Curuzu Cuatia","Corrientes","Argentina","34470"));
            ciudades.Add(new Ciudad("Bell Ville","Cordoba","Argentina","34439"));
            ciudades.Add(new Ciudad("Rio Segundo - Pilar​","Cordoba","Argentina","34423"));
            ciudades.Add(new Ciudad("Chilecito","La Rioja","Argentina","33724"));
            ciudades.Add(new Ciudad("Chascomus","Buenos Aires","Argentina","33607"));
            ciudades.Add(new Ciudad("Mercedes","Corrientes","Argentina","33551"));
            ciudades.Add(new Ciudad("Trenque Lauquen","Buenos Aires","Argentina","33442"));
            ciudades.Add(new Ciudad("Bragado","Buenos Aires","Argentina","33222"));
            ciudades.Add(new Ciudad("Centenario","Neuquen","Argentina","32928"));
            ciudades.Add(new Ciudad("Aguilares","Tucuman","Argentina","32908"));
            ciudades.Add(new Ciudad("Villaguay","Entre Rios","Argentina","32881"));
            ciudades.Add(new Ciudad("Chajari","Entre Rios","Argentina","32734"));
            ciudades.Add(new Ciudad("Esquel","Chubut","Argentina","32343"));
            ciudades.Add(new Ciudad("Termas de Rio Hondo","Santiago del Estero","Argentina","32166"));
            ciudades.Add(new Ciudad("Zapala","Neuquen","Argentina","32097"));
            ciudades.Add(new Ciudad("Pehuajo","Buenos Aires","Argentina","31533"));
            ciudades.Add(new Ciudad("General Güemes","Salta","Argentina","31494"));
            ciudades.Add(new Ciudad("Rivadavia","Mendoza","Argentina","31038"));
            ciudades.Add(new Ciudad("Cruz del Eje","Cordoba","Argentina","30680"));
            ciudades.Add(new Ciudad("Gobernador Virasoro","Corrientes","Argentina","30666"));
            ciudades.Add(new Ciudad("Victoria","Entre Rios","Argentina","30623"));
            ciudades.Add(new Ciudad("Villa Regina","Rio Negro","Argentina","30028"));
            ciudades.Add(new Ciudad("Cañuelas", "Buenos Aires", "Argentina", "29974"));
            ciudades.Add(new Ciudad("General Alvear​", "Mendoza", "Argentina", "29909"));
            ciudades.Add(new Ciudad("Lobos", "Buenos Aires", "Argentina", "29868"));
            ciudades.Add(new Ciudad("Miramar - El Marquesado", "Buenos Aires", "Argentina", "29629"));
            ciudades.Add(new Ciudad("Villa Gesell", "Buenos Aires", "Argentina", "29593"));
            ciudades.Add(new Ciudad("Cañada de Gomez", "Santa Fe", "Argentina", "29205"));
            ciudades.Add(new Ciudad("Bella Vista", "Corrientes", "Argentina", "29071"));
            ciudades.Add(new Ciudad("Tunuyan", "Mendoza", "Argentina", "28859"));
            ciudades.Add(new Ciudad("Baradero", "Buenos Aires", "Argentina", "28537"));
            ciudades.Add(new Ciudad("Mar de Ajo - San Bernardo​", "Buenos Aires", "Argentina", "28466"));
            ciudades.Add(new Ciudad("Metan", "Salta", "Argentina", "28295"));
            ciudades.Add(new Ciudad("Caucete", "San Juan", "Argentina", "28222"));
            ciudades.Add(new Ciudad("General Jose de San Martin", "Chaco", "Argentina", "28124"));
            ciudades.Add(new Ciudad("Lincoln", "Buenos Aires", "Argentina", "28051"));
            ciudades.Add(new Ciudad("San Martin de los Andes", "Neuquen", "Argentina", "27956"));
            ciudades.Add(new Ciudad("Salto", "Buenos Aires", "Argentina", "27466"));
            ciudades.Add(new Ciudad("Castelli", "Chaco", "Argentina", "27201"));
            ciudades.Add(new Ciudad("Marcos Juarez", "Cordoba", "Argentina", "27004"));
            ciudades.Add(new Ciudad("Saladillo", "Buenos Aires", "Argentina", "26763"));
            ciudades.Add(new Ciudad("Frias", "Santiago del Estero", "Argentina", "26649"));
            ciudades.Add(new Ciudad("Charata", "Chaco", "Argentina", "26497"));
            ciudades.Add(new Ciudad("Arrecifes", "Buenos Aires", "Argentina", "26400"));
            ciudades.Add(new Ciudad("San Carlos de Bolivar", "Buenos Aires", "Argentina", "26242"));
            ciudades.Add(new Ciudad("Dolores", "Buenos Aires", "Argentina", "25940"));
            ciudades.Add(new Ciudad("Pinamar​", "Buenos Aires", "Argentina", "25397"));
            ciudades.Add(new Ciudad("Rawson", "Chubut", "Argentina", "24616"));
            ciudades.Add(new Ciudad("Quitilipi", "Chaco", "Argentina", "24517"));
            ciudades.Add(new Ciudad("La Paz", "Entre Rios", "Argentina", "24307"));
            ciudades.Add(new Ciudad("Rosario de la Frontera", "Salta", "Argentina", "24140"));
            ciudades.Add(new Ciudad("Apostoles", "Misiones", "Argentina", "24083"));
            ciudades.Add(new Ciudad("Coronel Suarez", "Buenos Aires", "Argentina", "23621"));
            ciudades.Add(new Ciudad("Santa Teresita - Mar del Tuyu", "Buenos Aires", "Argentina", "23581"));
            ciudades.Add(new Ciudad("Monte Caseros", "Corrientes", "Argentina", "23470"));
            ciudades.Add(new Ciudad("25 de mayo", "Buenos Aires", "Argentina", "23408"));
            ciudades.Add(new Ciudad("Leandro N Alem", "Misiones", "Argentina", "23339"));
            ciudades.Add(new Ciudad("Santo Tome", "Corrientes", "Argentina", "23299"));
            ciudades.Add(new Ciudad("Añatuya", "Santiago del Estero", "Argentina", "23286"));
            ciudades.Add(new Ciudad("Monteros", "Tucuman", "Argentina", "23274"));
            ciudades.Add(new Ciudad("Colon", "Buenos Aires", "Argentina", "23206"));
            ciudades.Add(new Ciudad("Colon", "Entre Rios", "Argentina", "23150"));
            ciudades.Add(new Ciudad("Las Breñas", "Chaco", "Argentina", "22953"));
            ciudades.Add(new Ciudad("Famailla", "Tucuman", "Argentina", "22924"));
            ciudades.Add(new Ciudad("Allen", "Rio Negro", "Argentina", "22859"));
            ciudades.Add(new Ciudad("Nogoya", "Entre Rios", "Argentina", "22824"));
            ciudades.Add(new Ciudad("Cinco Saltos", "Rio Negro", "Argentina", "22790"));
            ciudades.Add(new Ciudad("Jardin America", "Misiones", "Argentina", "22762"));
            ciudades.Add(new Ciudad("Pichanal", "Salta", "Argentina", "22439"));
            ciudades.Add(new Ciudad("Arroyito", "Cordoba", "Argentina", "22147"));
            ciudades.Add(new Ciudad("Villa San Martin", "San Juan", "Argentina", "22046"));
            ciudades.Add(new Ciudad("Machagai", "Chaco", "Argentina", "21997"));
            ciudades.Add(new Ciudad("San Justo", "Santa Fe", "Argentina", "21624"));
            ciudades.Add(new Ciudad("Malargüe", "Mendoza", "Argentina", "21619"));
            ciudades.Add(new Ciudad("Las Flores", "Buenos Aires", "Argentina", "21455"));
            ciudades.Add(new Ciudad("Dean Funes", "Cordoba", "Argentina", "21211"));
            ciudades.Add(new Ciudad("Lules", "Tucuman", "Argentina", "21088"));
            ciudades.Add(new Ciudad("San Vicente", "Misiones", "Argentina", "21068"));
            ciudades.Add(new Ciudad("Pico Truncado", "Santa Cruz", "Argentina", "20889"));
            ciudades.Add(new Ciudad("Embarcacion", "Salta", "Argentina", "20843"));
            ciudades.Add(new Ciudad("Rosario de Lerma", "Salta", "Argentina", "20795"));
            ciudades.Add(new Ciudad("Arroyo Seco", "Santa Fe", "Argentina", "20620"));
            ciudades.Add(new Ciudad("Sunchales", "Santa Fe", "Argentina", "20537"));
            ciudades.Add(new Ciudad("Laboulaye", "Cordoba", "Argentina", "20534"));
            ciudades.Add(new Ciudad("Pirane", "Formosa", "Argentina", "20335"));
            ciudades.Add(new Ciudad("Coronel Pringles", "Buenos Aires", "Argentina", "20263"));
            ciudades.Add(new Ciudad("Brandsen", "Buenos Aires", "Argentina", "19877"));
            ciudades.Add(new Ciudad("Delfin Gallo - Colombres - La Florida​", "Tucuman", "Argentina", "19873"));
            ciudades.Add(new Ciudad("San Antonio de Areco", "Buenos Aires", "Argentina", "19768"));
            ciudades.Add(new Ciudad("Rojas", "Buenos Aires", "Argentina", "19766"));
            ciudades.Add(new Ciudad("Firmat", "Santa Fe", "Argentina", "19757"));
            ciudades.Add(new Ciudad("Ituzaingo", "Corrientes", "Argentina", "19575"));
            ciudades.Add(new Ciudad("Crespo", "Entre Rios", "Argentina", "19536"));
            ciudades.Add(new Ciudad("Vera", "Santa Fe", "Argentina", "19185"));
            ciudades.Add(new Ciudad("Diamante", "Entre Rios", "Argentina", "19142"));
            ciudades.Add(new Ciudad("Esquina", "Corrientes", "Argentina", "19081"));
            ciudades.Add(new Ciudad("Galvez", "Santa Fe", "Argentina", "19061"));
            ciudades.Add(new Ciudad("Salvador Mazza", "Salta", "Argentina", "18899"));
            ciudades.Add(new Ciudad("Montecarlo", "Misiones", "Argentina", "18827"));
            ciudades.Add(new Ciudad("Rufino", "Santa Fe", "Argentina", "18727"));
            ciudades.Add(new Ciudad("Rincon de los Sauces", "Neuquen", "Argentina", "18691"));
            ciudades.Add(new Ciudad("Juan Bautista Alberdi", "Tucuman", "Argentina", "18430"));
            ciudades.Add(new Ciudad("Carlos Casares", "Buenos Aires", "Argentina", "18347"));
            ciudades.Add(new Ciudad("General Villegas", "Buenos Aires", "Argentina", "18275"));
            ciudades.Add(new Ciudad("General Juan Madariaga", "Buenos Aires", "Argentina", "18089"));
            ciudades.Add(new Ciudad("Las Heras", "Santa Cruz", "Argentina", "17821"));
            ciudades.Add(new Ciudad("Santa Elena", "Entre Rios", "Argentina", "17791"));
            ciudades.Add(new Ciudad("San Jorge", "Santa Fe", "Argentina", "17615"));
            ciudades.Add(new Ciudad("Catriel", "Rio Negro", "Argentina", "17584"));
            ciudades.Add(new Ciudad("Ayacucho", "Buenos Aires", "Argentina", "17364"));
            ciudades.Add(new Ciudad("Merlo", "San Luis", "Argentina", "17084"));
            ciudades.Add(new Ciudad("El Bolson", "Rio Negro", "Argentina", "17061"));
            ciudades.Add(new Ciudad("San Miguel del Monte", "Buenos Aires", "Argentina", "17005"));
            ciudades.Add(new Ciudad("Tres Isletas", "Chaco", "Argentina", "16976"));
            ciudades.Add(new Ciudad("Morteros", "Cordoba", "Argentina", "16890"));
            ciudades.Add(new Ciudad("La Quiaca", "Jujuy", "Argentina", "16874"));
            ciudades.Add(new Ciudad("Coronda", "Santa Fe", "Argentina", "16873"));
            ciudades.Add(new Ciudad("Federacion", "Entre Rios", "Argentina", "16658"));
            ciudades.Add(new Ciudad("El Calafate", "Santa Cruz", "Argentina", "16655"));
            ciudades.Add(new Ciudad("San Jose", "Entre Rios", "Argentina", "16336"));
            ciudades.Add(new Ciudad("Joaquin Victor Gonzalez", "Salta", "Argentina", "16329"));
            ciudades.Add(new Ciudad("Carcaraña", "Santa Fe", "Argentina", "16277"));
            ciudades.Add(new Ciudad("San Antonio Oeste", "Rio Negro", "Argentina", "16265"));
            ciudades.Add(new Ciudad("San Andres de Giles", "Buenos Aires", "Argentina", "16243"));
            ciudades.Add(new Ciudad("Las Varillas", "Cordoba", "Argentina", "16238"));
            ciudades.Add(new Ciudad("Federal", "Entre Rios", "Argentina", "16075"));
            ciudades.Add(new Ciudad("Puerto Rico", "Misiones", "Argentina", "15995"));
            ciudades.Add(new Ciudad("Colonia Santa Rosa", "Salta", "Argentina", "15562"));
            ciudades.Add(new Ciudad("Aberastain-La Rinconada​", "San Juan", "Argentina", "15409"));
            ciudades.Add(new Ciudad("General Belgrano", "Buenos Aires", "Argentina", "15394"));
            ciudades.Add(new Ciudad("Villa del Rosario", "Cordoba", "Argentina", "15313"));
            ciudades.Add(new Ciudad("General Mosconi", "Salta", "Argentina", "15295"));
            ciudades.Add(new Ciudad("Esperanza", "Misiones", "Argentina", "15204"));
            ciudades.Add(new Ciudad("Quimili", "Santiago del Estero", "Argentina", "15052"));
            ciudades.Add(new Ciudad("Villa Ocampo", "Santa Fe", "Argentina", "15037"));
            ciudades.Add(new Ciudad("Mina Clavero - Villa Cura Brochero", "Cordoba", "Argentina", "14838"));
            ciudades.Add(new Ciudad("Bella Vista", "Tucuman", "Argentina", "14791"));
            ciudades.Add(new Ciudad("Villa Carmela", "Tucuman", "Argentina", "14728"));
            ciudades.Add(new Ciudad("Tostado", "Santa Fe", "Argentina", "14582"));
            ciudades.Add(new Ciudad("Ceres", "Santa Fe", "Argentina", "14499"));
            ciudades.Add(new Ciudad("Los Toldos", "Buenos Aires", "Argentina", "14496"));
            ciudades.Add(new Ciudad("San Cristobal", "Santa Fe", "Argentina", "14389"));
            ciudades.Add(new Ciudad("Pigüe", "Buenos Aires", "Argentina", "14383"));
            ciudades.Add(new Ciudad("Benito Juarez", "Buenos Aires", "Argentina", "14279"));
            ciudades.Add(new Ciudad("El Colorado", "Formosa", "Argentina", "14228"));
            ciudades.Add(new Ciudad("Puerto Deseado", "Santa Cruz", "Argentina", "14183"));
            ciudades.Add(new Ciudad("Wanda", "Misiones", "Argentina", "13901"));
            ciudades.Add(new Ciudad("Candelaria", "Misiones", "Argentina", "13777"));
            ciudades.Add(new Ciudad("La Leonesa - Las Palmas", "Chaco", "Argentina", "13737"));
            ciudades.Add(new Ciudad("Cafayate", "Salta", "Argentina", "13698"));
            ciudades.Add(new Ciudad("Rio Colorado - La Adela", "Rio Negro – La Pampa", "Argentina", "13637"));
            ciudades.Add(new Ciudad("El Carmen", "Jujuy", "Argentina", "13623"));
            ciudades.Add(new Ciudad("San Javier", "Santa Fe", "Argentina", "13604"));
            ciudades.Add(new Ciudad("Ramallo", "Buenos Aires", "Argentina", "13319"));
            ciudades.Add(new Ciudad("Fraile Pintado", "Jujuy", "Argentina", "13300"));
            ciudades.Add(new Ciudad("Navarro", "Buenos Aires", "Argentina", "13224"));
            ciudades.Add(new Ciudad("Tupungato​", "Mendoza", "Argentina", "13218"));
            ciudades.Add(new Ciudad("La Punta", "San Luis", "Argentina", "13182"));
            ciudades.Add(new Ciudad("Oncativo", "Cordoba", "Argentina", "13180"));
            ciudades.Add(new Ciudad("Chos Malal", "Neuquen", "Argentina", "13092"));
            ciudades.Add(new Ciudad("Capitan Sarmiento", "Buenos Aires", "Argentina", "13088"));
            ciudades.Add(new Ciudad("Las Rosas", "Santa Fe", "Argentina", "13068"));
            ciudades.Add(new Ciudad("General Pinedo", "Chaco", "Argentina", "13042"));
            ciudades.Add(new Ciudad("Loberia", "Buenos Aires", "Argentina", "13005"));
            ciudades.Add(new Ciudad("Chamical", "La Rioja", "Argentina", "12919"));
            ciudades.Add(new Ciudad("Fernandez", "Santiago del Estero", "Argentina", "12886"));
            ciudades.Add(new Ciudad("Saladas", "Corrientes", "Argentina", "12864"));
            ciudades.Add(new Ciudad("Rosario del Tala", "Entre Rios", "Argentina", "12801"));
            ciudades.Add(new Ciudad("Ingeniero Juarez", "Formosa", "Argentina", "12798"));
            ciudades.Add(new Ciudad("Carmen de Areco", "Buenos Aires", "Argentina", "12775"));
            ciudades.Add(new Ciudad("San Salvador", "Entre Rios", "Argentina", "12733"));
            ciudades.Add(new Ciudad("Rauch", "Buenos Aires", "Argentina", "12705"));
            ciudades.Add(new Ciudad("San Carlos Centro - San Carlos Sud​", "Santa Fe", "Argentina", "12644"));
            ciudades.Add(new Ciudad("Junin de los Andes", "Neuquen", "Argentina", "12621"));
            ciudades.Add(new Ciudad("Andalgala", "Catamarca", "Argentina", "12600"));
            ciudades.Add(new Ciudad("Monte Quemado", "Santiago del Estero", "Argentina", "12543"));
            ciudades.Add(new Ciudad("La Carlota", "Cordoba", "Argentina", "12537"));
            ciudades.Add(new Ciudad("Las Lomitas", "Formosa", "Argentina", "12399"));
            ciudades.Add(new Ciudad("Santa Rosa de Calamuchita", "Cordoba", "Argentina", "12395"));
            ciudades.Add(new Ciudad("Las Parejas", "Santa Fe", "Argentina", "12375"));
            ciudades.Add(new Ciudad("Aristobulo del Valle", "Misiones", "Argentina", "12375"));
            ciudades.Add(new Ciudad("Laguna Paiva", "Santa Fe", "Argentina", "12334"));
            ciudades.Add(new Ciudad("Rodeo del Medio", "Mendoza", "Argentina", "12327"));
            ciudades.Add(new Ciudad("San Luis del Palmar", "Corrientes", "Argentina", "12287"));
            ciudades.Add(new Ciudad("Belen", "Catamarca", "Argentina", "12256"));
            ciudades.Add(new Ciudad("Aimogasta", "La Rioja", "Argentina", "12249"));
            ciudades.Add(new Ciudad("General Acha", "La Pampa", "Argentina", "12184"));
            ciudades.Add(new Ciudad("San Clemente del Tuyu", "Buenos Aires", "Argentina", "12126"));
            ciudades.Add(new Ciudad("Daireaux", "Buenos Aires", "Argentina", "12122"));
            ciudades.Add(new Ciudad("Cerrillos", "Salta", "Argentina", "11869"));
            ciudades.Add(new Ciudad("Recreo", "Catamarca", "Argentina", "11847"));
            ciudades.Add(new Ciudad("General Cabrera", "Cordoba", "Argentina", "11734"));
            ciudades.Add(new Ciudad("America", "Buenos Aires", "Argentina", "11685"));
            ciudades.Add(new Ciudad("Oliva", "Cordoba", "Argentina", "11672"));
            ciudades.Add(new Ciudad("Santa Maria", "Catamarca", "Argentina", "11648"));
            ciudades.Add(new Ciudad("Monterrico", "Jujuy", "Argentina", "11591"));
            ciudades.Add(new Ciudad("Santa Lucia", "Corrientes", "Argentina", "11589"));
            ciudades.Add(new Ciudad("Coronel Dorrego", "Buenos Aires", "Argentina", "11510"));
            ciudades.Add(new Ciudad("Tinogasta", "Catamarca", "Argentina", "11485"));
            ciudades.Add(new Ciudad("Comandante Andresito", "Misiones", "Argentina", "11482"));
            ciudades.Add(new Ciudad("Almafuerte", "Cordoba", "Argentina", "11422"));
            ciudades.Add(new Ciudad("General Las Heras", "Buenos Aires", "Argentina", "11331"));
            ciudades.Add(new Ciudad("Villa Ramallo", "Buenos Aires", "Argentina", "11280"));
            ciudades.Add(new Ciudad("Armstrong", "Santa Fe", "Argentina", "11181"));
            ciudades.Add(new Ciudad("Capilla del Monte", "Cordoba", "Argentina", "11181"));
            ciudades.Add(new Ciudad("Magdalena", "Buenos Aires", "Argentina", "11093"));
            ciudades.Add(new Ciudad("El Trebol", "Santa Fe", "Argentina", "11086"));
            ciudades.Add(new Ciudad("Villa La Angostura", "Neuquen", "Argentina", "11063"));
            ciudades.Add(new Ciudad("General Deheza", "Cordoba", "Argentina", "11061"));
            ciudades.Add(new Ciudad("Chepes", "La Rioja", "Argentina", "11039"));
            ciudades.Add(new Ciudad("Loreto", "Santiago del Estero", "Argentina", "10996"));
            ciudades.Add(new Ciudad("San Jose de Jachal", "San Juan", "Argentina", "10940"));
            ciudades.Add(new Ciudad("Sarmiento", "Chubut", "Argentina", "10858"));
            ciudades.Add(new Ciudad("Las Toscas", "Santa Fe", "Argentina", "10838"));
            ciudades.Add(new Ciudad("Aguaray", "Salta", "Argentina", "10410"));
            ciudades.Add(new Ciudad("Corral de Bustos", "Cordoba", "Argentina", "10407"));
            ciudades.Add(new Ciudad("San Pedro", "Misiones", "Argentina", "10397"));
            ciudades.Add(new Ciudad("Leones", "Cordoba", "Argentina", "10391"));
            ciudades.Add(new Ciudad("Roque Perez", "Buenos Aires", "Argentina", "10358"));
            ciudades.Add(new Ciudad("Corzuela", "Chaco", "Argentina", "10335"));
            ciudades.Add(new Ciudad("Totoras", "Santa Fe", "Argentina", "10292"));
            ciudades.Add(new Ciudad("San Jose de Feliciano", "Entre Rios", "Argentina", "10282"));
            ciudades.Add(new Ciudad("Villa Elisa", "Entre Rios", "Argentina", "10266"));
            ciudades.Add(new Ciudad("Humahuaca", "Jujuy", "Argentina", "10256"));
            ciudades.Add(new Ciudad("Villa Berthet", "Chaco", "Argentina", "10224"));
            ciudades.Add(new Ciudad("Lima", "Buenos Aires", "Argentina", "10219"));
            ciudades.Add(new Ciudad("Hipolito Yrigoyen", "Salta", "Argentina", "10196"));
            ciudades.Add(new Ciudad("Batan", "Buenos Aires", "Argentina", "10152"));
            ciudades.Add(new Ciudad("Choele Choel", "Rio Negro", "Argentina", "10146"));
            ciudades.Add(new Ciudad("Justo Daract", "San Luis", "Argentina", "10135"));
            ciudades.Add(new Ciudad("Hernando", "Cordoba", "Argentina", "10109"));
            ciudades.Add(new Ciudad("Vicuña Mackenna", "Cordoba", "Argentina", "10021"));


            return ciudades;
        }
    }
}