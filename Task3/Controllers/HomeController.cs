using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Task3.Models;

namespace Task3.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private IGoogleRecaptchaService _googleService;

        public HomeController() { }

        public HomeController(IGoogleRecaptchaService service)
        {
            _googleService = service;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
