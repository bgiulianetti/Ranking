using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class Bordereaux
    {
        public List<EntradasBorderaux> Entradas { get; set; }
        public int EntradasTotal { get; set; }
        public float EntradasBruto { get; set; }

        public List<ImpuestosDeduccionesTeatroBorderaux> ImpuestosDeduccionesTeatro { get; set; }
        public float ImpuestosDeduccionesBruto { get; set; }
        public float ImpuestosDeduccionesTotalDeducir { get; set; }
        public float ImpuestosDeduccionesNeto { get; set; }

        public float ImpuestosDeduccionesCompanyPorcentaje { get; set; }
        public float ImpuestosDeduccionesCompanyMonto { get; set; }
        public float ImpuestosDeduccionesTeatroPorcentaje { get; set; }
        public float ImpuestosDeduccionesTeatroMonto { get; set; }

        public bool ArregloFijo { get; set; }
        public List<GastosBordereaux> GastosCompany { get; set; }
        public float GastosCompanyTotal { get; set; }
        public float GastosCompanyNeto { get; set; }

        public float SUAPorcentaje { get; set; }
        public float SUAMonto { get; set; }
        public float ShowPorcentaje { get; set; }
        public float ShowMonto { get; set; }

        public string ArregloProductor { get; set; }
        public float ProductorPorcentaje { get; set; }
        public float ProductorMonto { get; set; }
        public float SUAMontoFinal { get; set; }

        public DateTime FechaCobro { get; set; }
        public string Forma { get; set; }
        public string Comentarios { get; set; }
    }
}