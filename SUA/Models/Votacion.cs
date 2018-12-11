using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class Votacion
    {
        public string Ip { get; set; }
        public string Nombre { get; set; }
        public string Show { get; set; }
        public DateTime Fecha { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public Ciudad Ciudad { get; set; }
        public string Notificaciones { get; set; }
        public string Descuentos { get; set; }
    }
}