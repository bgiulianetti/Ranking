using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class EntradasBorderaux
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public float Total
        {
            get; set;
            /*get { return Cantidad * Precio; }
            set { }*/
        }
    }
}