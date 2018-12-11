using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SUA.Models;
using Nest;
using Elasticsearch.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;

namespace SUA.Repositorios
{
    public class ESRepositorio
    {
        public const string INVALID_SETTINGS_EXCEPTION = "Configuración de ES inválida";
        //Logs
        public const string INVALID_LOG_ES_CONNECTION_EXCEPTION = "Falla al querer conectar con elasticsearch al querer obtener todos los logs";
        public const string LOG_GET_ALL_EXCEPTION = "Falla al querer obtener todos los logs";
        public const string LOG_CREATE_INVALID_PARAMETER_EXCEPTION = "Para agregar un log debe pasar un log válido";
        public const string LOG_CREATE_NOT_CREATED_EXCEPTION = "Falla al querer crear un log nuevo";

        //Votacion
        public const string INVALID_VOTACION_ES_CONNECTION_EXCEPTION = "db_connection_error";
        public const string VOTACION_GET_ALL_EXCEPTION = "get_votaciones_error";
        public const string VOTACION_GET_BY_IP_INVALID_PARAMETER_EXCEPTION = "invalid_ip";
        public const string VOTACION_GET_BY_IP_AND_SHOW_INVALID_IP_EXCEPTION = "invalid_ip__get_show_by_ip_and_show";
        public const string VOTACION_GET_BY_IP_AND_SHOW_SEARCH_EXCEPTION = "Error al querer buscar votaciones por ip y show";
        public const string VOTACION_GET_BY_SHOW_INVALID_PARAMETER_EXCEPTION = "invalid_show";
        public const string VOTACION_GET_BY_SHOW_AND_EMAIL_INVALID_PARAMETER_EXCEPTION = "invalid_email";
        public const string VOTACION_GET_BY_SHOW_AND_TEL_INVALID_PARAMETER_EXCEPTION = "invalid_tel";
        public const string VOTACION_GET_BY_SHOW_INVALID_SEARCH_EXCEPTION = "get_votacion_by_show_error";
        public const string VOTACION_GET_BY_SHOW_AND_TEL_INVALID_SEARCH_EXCEPTION = "get_votacion_by_show_and_tel_search_error";
        public const string VOTACION_GET_BY_SHOW_AND_MAIL_INVALID_SEARCH_EXCEPTION = "get_votacion_by_show_and_mail_search_error";
        public const string VOTACION_CREATE_INVALID_PARAMETER_EXCEPTION = "add_votacion_invalid_parameter_error";
        public const string VOTACION_CANT_MAX_EXCEPTION = "voto_ya_registrado_error";
        public const string VOTACION_CREATE_NOT_CREATED_EXCEPTION = "add_votacion_error";


        protected ElasticClient Client { get; set; }
        protected string Index { get; set; }

        public ESRepositorio(ESSettings settings, string index)
        {
            if (settings == null)
                throw new Exception(INVALID_SETTINGS_EXCEPTION);

            var config = new ConnectionSettings(settings.Node.Uri);
            Client = new ElasticClient(config);

            Index = index;
        }


        /*-------------------Logs-------------------*/
        public List<Log> GetLogs()
        {
            var response = Client.Search<Log>(s => s
                   .Index(Index)
                   .Type(Index)
                   .From(0)
                   .Size(GetCount(Index))
                  );

            if (response == null)
                throw new Exception(INVALID_LOG_ES_CONNECTION_EXCEPTION);

            if (!response.IsValid)
                throw new Exception(LOG_GET_ALL_EXCEPTION);

            var logs = new List<Log>();
            if (response.Total > 0)
            {
                foreach (var item in response.Documents)
                    logs.Add(item);
            }
            return logs;
        }
        public void AddLog(Log log)
        {
            if (log == null)
                throw new Exception(LOG_CREATE_INVALID_PARAMETER_EXCEPTION);

            if (!IndexExists())
                CreateIndex();


            var response = Client.IndexAsync(log, i => i
              .Index(Index)
              .Type(Index)
              .Refresh(Refresh.True)
              ).Result;

            if (!response.IsValid)
                throw new Exception(LOG_CREATE_NOT_CREATED_EXCEPTION);
        }
        public void AddBulkLog(List<Log> logs)
        {
            if (!IndexExists())
                CreateIndex();

            var response = Client.IndexManyAsync(logs, Index, Index).Result;

            if (!response.IsValid)
                throw new Exception(LOG_CREATE_NOT_CREATED_EXCEPTION);
        }


        /*-------------------Votacion-------------------*/

        public List<Votacion> GetVotacionesViejo(int skip, int take)
        {
            var response = Client.Search<Votacion>(s => s
                   .Index(Index)
                   .Type(Index)
                   .From(skip)
                   .Size(take)
                  );

            if (response == null)
                throw new Exception(INVALID_VOTACION_ES_CONNECTION_EXCEPTION);

            if (!response.IsValid)
                throw new Exception(VOTACION_GET_ALL_EXCEPTION);

            var votaciones = new List<Votacion>();
            if (response.Total > 0)
            {
                foreach (var item in response.Documents)
                    votaciones.Add(item);
            }
            return votaciones;
        }
        public List<Votacion> GetVotacionesByShowViejo(string show)
        {
            if (string.IsNullOrEmpty(show))
                throw new Exception(VOTACION_GET_BY_SHOW_INVALID_PARAMETER_EXCEPTION);

            var response = Client.Search<Votacion>(s => s
                   .Index(Index)
                   .Type(Index)
                   .From(0)
                   .Size(9999)
                   .Query(q => q
                   .Match(m => m.Field(f => f.Show).Query(show)))
                   );

            if (response == null)
                return null;

            if (!response.IsValid)
                throw new Exception(VOTACION_GET_BY_SHOW_INVALID_SEARCH_EXCEPTION);

            var votaciones = new List<Votacion>();
            if (response.Total > 0)
            {
                foreach (var item in response.Documents)
                    if (show == item.Show)
                        votaciones.Add(item);
            }
            return votaciones;
        }
        public Votacion GetVotacionesByEmailAndShow(string email, string show)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception(VOTACION_GET_BY_SHOW_AND_EMAIL_INVALID_PARAMETER_EXCEPTION);

            var response = Client.Search<Votacion>(s => s
                   .Index(Index)
                   .Type(Index)
                   .Query(q => q
                    .Match(m => m.Field(f => f.Email).Query(email)))
                    );

            if (response == null)
                return null;

            if (!response.IsValid)
                throw new Exception(VOTACION_GET_BY_SHOW_AND_MAIL_INVALID_SEARCH_EXCEPTION);

            Votacion votacion = null;
            if (response.Total > 0)
            {
                foreach (var item in response.Documents)
                    if (email == item.Email && show == item.Show)
                    {
                        votacion = item;
                        break;
                    }
            }
            return votacion;
        }
        public Votacion GetVotacionesByTelAndShow(string tel, string show)
        {
            if (string.IsNullOrEmpty(tel))
                throw new Exception(VOTACION_GET_BY_SHOW_AND_TEL_INVALID_PARAMETER_EXCEPTION);

            var response = Client.Search<Votacion>(s => s
                   .Index(Index)
                   .Type(Index)
                   .Query(q => q
                    .Match(m => m.Field(f => f.Telefono).Query(tel)))
                    );

            if (response == null)
                return null;

            if (!response.IsValid)
                throw new Exception(VOTACION_GET_BY_SHOW_AND_TEL_INVALID_SEARCH_EXCEPTION);

            Votacion votacion = null;
            if (response.Total > 0)
            {
                foreach (var item in response.Documents)
                    if (tel == item.Telefono && show == item.Show)
                    {
                        votacion = item;
                        break;
                    }
            }
            return votacion;
        }
        public List<Votacion> GetVotacionesByIpAndShow(string ip, string show)
        {
            if (string.IsNullOrEmpty(ip))
                throw new Exception(VOTACION_GET_BY_IP_AND_SHOW_INVALID_IP_EXCEPTION);

            var response = Client.Search<Votacion>(s => s
                   .Index(Index)
                   .Type(Index)
                   .Query(q => q.Term("ip", ip)));

            if (response == null)
                return null;

            if (!response.IsValid)
                throw new Exception(VOTACION_GET_BY_IP_AND_SHOW_SEARCH_EXCEPTION);

            var votaciones = new List<Votacion>();
            if (response.Total > 0)
            {
                foreach (var item in response.Documents)
                    if (ip == item.Ip && show == item.Show)
                        votaciones.Add(item);
            }
            return votaciones;
        }
        public void AddVotacion(Votacion votacion)
        {
            if (votacion == null)
                throw new Exception(VOTACION_CREATE_INVALID_PARAMETER_EXCEPTION);

            if (!IndexExists())
                CreateIndex();

            var votaciones = GetVotacionesByIpAndShow(votacion.Ip, votacion.Show);
            if (votaciones != null && votaciones.Count >= 3)
                throw new Exception(VOTACION_CANT_MAX_EXCEPTION);

            var votacionObtenida = GetVotacionesByEmailAndShow(votacion.Email, votacion.Show);
            if (votacionObtenida != null)
                throw new Exception(VOTACION_CANT_MAX_EXCEPTION);

            var votacionObtenidaTel = GetVotacionesByTelAndShow(votacion.Telefono, votacion.Show);
            if (votacionObtenidaTel != null)
                throw new Exception(VOTACION_CANT_MAX_EXCEPTION);

            var response = Client.IndexAsync(votacion, i => i
              .Index(Index)
              .Type(Index)
              .Refresh(Refresh.True)
              ).Result;

            if (!response.IsValid)
                throw new Exception(VOTACION_CREATE_NOT_CREATED_EXCEPTION);
        }
        public void AddBulkVotacion(List<Votacion> votaciones)
        {
            if (!IndexExists())
                CreateIndex();

            var response = Client.IndexManyAsync(votaciones, Index, Index).Result;

            if (!response.IsValid)
                throw new Exception(VOTACION_CREATE_NOT_CREATED_EXCEPTION);
        }

        public List<Votacion> GetVotaciones()
        {
            var response = Client.Search<Votacion>
                (scr => scr.Index(Index)
                     .From(0)
                     .Take(10000)
                     .MatchAll()
                     .Scroll("2m"));
            var votaciones = new List<Votacion>();

            if (!response.IsValid || string.IsNullOrEmpty(response.ScrollId))
                throw new Exception(VOTACION_GET_ALL_EXCEPTION);

            if (response.Documents.Any())
                votaciones.AddRange(response.Documents);

            string scrollid = response.ScrollId;
            bool isScrollSetHasData = true;
            while (isScrollSetHasData)
            {
                var loopingResponse = Client.Scroll<Votacion>("2m", scrollid);
                if (loopingResponse.IsValid)
                {
                    votaciones.AddRange(loopingResponse.Documents);
                    scrollid = loopingResponse.ScrollId;
                }
                isScrollSetHasData = loopingResponse.Documents.Any();
            }

            Client.ClearScroll(new ClearScrollRequest(scrollid));
            return votaciones;
        }

        public List<Votacion> GetVotacionesByShow(string show)
        {
            var response = Client.Search<Votacion>
                (scr => scr.Index(Index)
                     .From(0)
                     .Take(10000)
                     .Query(q => q
                        .Match(m => m.Field(f => f.Show).Query(show)))
                     .Scroll("2m"));
            var votaciones = new List<Votacion>();

            if (!response.IsValid || string.IsNullOrEmpty(response.ScrollId))
                throw new Exception(VOTACION_GET_ALL_EXCEPTION);

            if (response.Documents.Any())
            {
                foreach (var item in response.Documents)
                {
                    if(item.Show == show)
                        votaciones.Add(item);
                }
            }

            string scrollid = response.ScrollId;
            bool isScrollSetHasData = true;
            while (isScrollSetHasData)
            {
                var loopingResponse = Client.Scroll<Votacion>("2m", scrollid);
                if (loopingResponse.IsValid)
                {
                    foreach (var item in loopingResponse.Documents)
                    {
                        votaciones.Add(item);
                    }
                    scrollid = loopingResponse.ScrollId;
                }
                isScrollSetHasData = loopingResponse.Documents.Any();
            }

            Client.ClearScroll(new ClearScrollRequest(scrollid));
            return votaciones;
        }

        /*---------Metodos genericos------------------*/

        public int GetCount(string tipo)
        {
            ICountResponse response = null;
            
            if (tipo == "log")
                response = Client.Count<Log>(c => c.Index(Index).Type(Index));
            else if (tipo == "votacion")
                response = Client.Count<Votacion>(c => c.Index(Index).Type(Index));
            return (int)response.Count;
        }
        public void DeleteIndex()
        {
            if (IndexExists())
            {
                var response = Client.DeleteIndex(Index);
                if (!response.IsValid)
                    throw new Exception("delte_index_exception");
            }
        }
        public bool IndexExists()
        {
            var response = Client.IndexExists(Index);
            return response.Exists;
        }
        public void CreateIndex()
        {
            Client.CreateIndex(Index, c => c
                       .Settings(s => s
                         .NumberOfShards(5)
                         .NumberOfReplicas(5))
                   );
        }

        public enum ContentType
        {
            log,
            votacion
        }
    }
}