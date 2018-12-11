using Nest;
using Elasticsearch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    [ElasticsearchType(Name = "standupero")]
    public class Standupero : Persona
    {
        public string InstagramUser { get; set; }
        public string Foto { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Standupero && (obj as Standupero).GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return Dni.GetHashCode();
        }
    }
}