using SUA.Models;
using SUA.Repositorios;
using System;
using System.Web;
using System.Collections.Generic;
using System.Web.Caching;
using System.Linq;

namespace SUA.Servicios
{
    public class VotacionService
    {
        public ESRepositorio Repository { get; set; }

        public VotacionService()
        {
            var node = new UriBuilder("localhost");
            node.Port = 9200;
            var settings = new ESSettings(node);
            Repository = new ESRepositorio(settings, ESRepositorio.ContentType.votacion.ToString());
        }

        public List<Votacion> GetVotaciones(/*string skip = null, string take = null*/)
        {
            return Repository.GetVotaciones();
            /*
            int _skip = 0;
            int _take = 0;
            if(skip != null)
            {
                _skip = Int32.Parse(skip);
                _take = Int32.Parse(take);
            }
            else
            {
                _take = Repository.GetCount(ESRepositorio.ContentType.votacion.ToString());
            }
            return Repository.GetVotaciones(_skip, _take);
            */
        }

        public int GetCount()
        {
            return Repository.GetCount(ESRepositorio.ContentType.votacion.ToString());
        }

        public void AddVotacion(Votacion votacion)
        {
            var votaciones = (List<Votacion>)HttpContext.Current.Cache["ips"];
            if(votaciones == null)
            {
                votaciones = new List<Votacion>();
                HttpContext.Current.Cache["ips"] = votaciones;
            }

            var fechaCampania = new DateTime(2020, 02, 10);
            var votosFiltrados = votaciones.FindAll(f => f.Show == votacion.Show).FindAll(f => f.Fecha > fechaCampania).FindAll(f => f.Ip == votacion.Ip).ToList();
            if (votosFiltrados.Count < 3)
            {
                if (votosFiltrados.FindAll(f => f.Email == votacion.Email).Count == 0)
                {
                    Repository.AddVotacion(votacion);
                    votaciones.Add(votacion);
                    HttpContext.Current.Cache["ips"] = votaciones;
                }
                else
                {
                    throw new Exception("voto_ya_registrado_error");
                }
            }
            else
            {
                throw new Exception("voto_ya_registrado_error");
            }
                

          
        }

        public void AddBulkVotacion(List<Votacion> votaciones)
        {
            Repository.AddBulkVotacion(votaciones);
        }

        public void GetVotacionesByShow(string show)
        {
            Repository.GetVotacionesByShow(show);
        }

        public List<RankingRecord> GetRankingByShow(string show, string full)
        {
            var ranking = new List<RankingRecord>();
            var votaciones = Repository.GetVotacionesByShow(show);
            foreach (var votacion in votaciones)
            {
                var CiudadObtenida = ranking.Find(f => f.Ciudad.Nombre == votacion.Ciudad.Nombre);
                if(CiudadObtenida != null)
                {
                    var index = ranking.FindIndex(c => c.Ciudad.Nombre == votacion.Ciudad.Nombre);
                    ranking[index] = new RankingRecord { Ciudad = CiudadObtenida.Ciudad, VotesCount = CiudadObtenida.VotesCount + 1 };
                }
                else
                {
                    var record = new RankingRecord { Ciudad = votacion.Ciudad, VotesCount = 1};
                    ranking.Add(record);
                }
            }
            foreach (var registro in ranking)
            {
                registro.VotesCount = registro.VotesCount * 100 / votaciones.Count;
            }
            var rankingOrdenado = ranking.OrderByDescending(f=>f.VotesCount).ToList();

            if (full == "full")
                return rankingOrdenado;
            else
                return rankingOrdenado.Take(100).ToList();
        }
    }
}