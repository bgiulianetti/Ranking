using SUA.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class Show
    {
        public string UniqueId { get; set; }
        public string _Show { get; set; }
        public string Nombre { get; set; }
        public List<Standupero> Integrantes { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Rider { get; set; }
        public string Camarin { get; set; }
        public string Observaciones { get; set; }
        public Productor Productor { get; set; }
        public string SiglaBordereaux { get; set; }
        public string BackgroundColorCalendar { get; set; }
        public string TextColorCalendar { get; set; }
        public string GoogleCalendarId { get; set; }
        public List<RepeticionPlazas> Repeticion  { get; set; }


        public void SetIdAndFechaAlta()
        {
            UniqueId = UtilitiesAndStuff.GenerateUniqueId();
            FechaAlta = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Show && (obj as Show).GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return UniqueId.GetHashCode();
        }
    }
}