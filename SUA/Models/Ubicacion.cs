using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class Ubicacion
    {
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
    }
}