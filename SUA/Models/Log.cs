using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class Log
    {
        public DateTime Fecha { get; set; }
        public string Username { get; set; }
        public string Pantalla { get; set; }
        public string Accion { get; set; }
        public string Informacion { get; set; }
    }
}