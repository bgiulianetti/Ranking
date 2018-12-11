using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class InfoPlazasParaRepeticion
    {
        public string Ciudad { get; set; }
        public int Repeticion { get; set; }
        public List<SalaSimple> Salas { get; set; }
    }

    public class SalaSimple
    {
        public string IdSala { get; set; }
        public string Nombre { get; set; }
    }
}