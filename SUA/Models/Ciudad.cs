using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class Ciudad
    {
        public Ciudad()
        { }

        public Ciudad(string nombre, string estado, string pais, string poblacion)
        {
            Nombre = nombre;
            Estado = estado;
            Pais = pais;
            Poblacion = poblacion;
        }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Poblacion { get; set; }
    }
}