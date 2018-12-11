using SUA.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class Hotel
    {
        public string UniqueId { get; set; }
        public string Nombre { get; set; }
        public Ubicacion Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Estrellas { get; set; }
        public string Restaurante { get; set; }
        public string PiletaClimatizada { get; set; }
        public string Garage { get; set; }
        public string UltimosPrecios { get; set; }
        public string Canje { get; set; }
        public string Arreglo { get; set; }
        public string ContactoNombre { get; set; }
        public string ContactoEmail { get; set; }
        public string ContactoWhatsApp { get; set; }
        public string UltimosCanjes { get; set; }


        public void SetId()
        {
            UniqueId = UtilitiesAndStuff.GenerateUniqueId();
        }
    }
}