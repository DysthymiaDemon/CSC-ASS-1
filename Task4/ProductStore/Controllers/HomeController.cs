using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductStore.Controllers
{
    public class HomeController : Controller
    {

        [RequireHttps]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [RequireHttps]
        public ActionResult Talents()
        {
            ViewBag.Title = "Talents Page";

            return View();
        }
    }
}
