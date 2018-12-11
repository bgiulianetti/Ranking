using SUA.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class Fecha
    {
        public string UniqueId { get; set; }
        public Show Show { get; set; }
        public Sala Sala { get; set; }
        public DateTime FechaHorario { get; set; }
        public Productor Productor { get; set; }
        public Bordereaux Borederaux { get; set; }

        public void SetIdAndFechaAlta()
        {
            UniqueId = UtilitiesAndStuff.GenerateUniqueId();
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Fecha && (obj as Fecha).GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return UniqueId.GetHashCode();
        }
    }
}