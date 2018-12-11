using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    [ElasticsearchType(Name = "productor")]
    public class Productor : Persona
    {
        public override bool Equals(object obj)
        {
            return obj != null && obj is Productor && (obj as Productor).GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return Dni.GetHashCode();
        }
    }
}