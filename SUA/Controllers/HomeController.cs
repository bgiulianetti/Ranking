using SUA.Models;
using SUA.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SUA.Utilities;
using Newtonsoft.Json;

namespace SUA.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.titulo = "Votación";
            return View();
        }   
    }
}