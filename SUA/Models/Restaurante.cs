using SUA.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class Restaurante
    {
        public string UniqueId { get; set; }
        public string Nombre { get; set; }
        public Ubicacion Direccion { get; set; }
        public string Contacto { get; set; }
        public string Email { get; set; }
        public string WhatsApp { get; set; }
        public string Instagram { get; set; }
        public string Arreglo { get; set; }

        public void SetId()
        {
            UniqueId = UtilitiesAndStuff.GenerateUniqueId();
        }
    }
}