using SUA.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class InfoSalasVistaAndShow
    {
        //Info Sala
        public string SalaId { get; set; }
        public int SalaRepeticionEnDias { get; set; }

        //show seleccionado
        public string ShowSeleccionadoNombre { get; set; }
        public DateTime ShowSeleccionadoUltimaFechaEnLaSala { get; set; }
        public double DiasDesdeUltimaFechaDeShowEspecifico
        {
            get { return (DateTime.Now - ShowSeleccionadoUltimaFechaEnLaSala).TotalDays; }
            protected set { }
        }

        //Cualquier show
        public string UltimoShowPresentadoEnLaSala { get; set; }
        public DateTime UltimaFechaRealizadaEnLaSala { get; set; }
        public double DiasDesdeUltimaFechaRealizada
        {
            get { return (DateTime.Now - UltimaFechaRealizadaEnLaSala).TotalDays; }
            protected set { }
        }
        public int DiferenciaEnDiasVencimiento { get; set; }

    }
}