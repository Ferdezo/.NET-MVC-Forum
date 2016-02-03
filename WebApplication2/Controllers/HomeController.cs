using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Topic");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Forum z wykorzystaniem ASP.NET MVC";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Arkadiusz Migała grupa OS\n arkamig314@student.polsl.pl";

            return View();
        }
    }
}