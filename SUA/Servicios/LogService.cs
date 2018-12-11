using SUA.Models;
using SUA.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Servicios
{
    public class LogService
    {
        public ESRepositorio Repository { get; set; }

        public LogService()
        {
            var node = new UriBuilder("localhost");
            node.Port = 9200;
            var settings = new ESSettings(node);
            Repository = new ESRepositorio(settings, ESRepositorio.ContentType.log.ToString());
        }

        public List<Log> GetLogs()
        {
            return Repository.GetLogs();
        }

        public void AddLog(Log log)
        {
            Repository.AddLog(log);
        }

        public void AddBulkLog(List<Log> logs)
        {
            Repository.AddBulkLog(logs);
        }

        public void FormatAndSaveLog(string pantalla, string accion, string informacion)
        {
            var log = new Log
            {
                Accion = accion,
                Fecha = DateTime.Now,
                Informacion = informacion,
                Pantalla = pantalla,
                Username = HttpContext.Current.Request.Cookies.Get("session").Value
            };
            AddLog(log);
        }
    }
}