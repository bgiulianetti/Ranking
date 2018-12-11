using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class Persona
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public Ubicacion Direccion { get; set; }
        public Ubicacion DireccionFacturacion { get; set; }
        public DatosBancarios DatosBancarios { get; set; }
        public string Observaciones { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string TransportePropio { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}