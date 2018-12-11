using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class DatosBancarios
    {
        public string NombreCompleto { get; set; }
        public string CuilCuit { get; set; }
        public string Cbu { get; set; }
        public string Alias { get; set; }
        public string Banco { get; set; }
        public string TipoCuenta { get; set; }
    }
}