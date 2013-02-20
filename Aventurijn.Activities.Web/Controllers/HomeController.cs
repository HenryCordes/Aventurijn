using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aventurijn.Activities.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Een manier om bij te houden wat geleerd wordt";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Contact met de Aventurijn";

            return View();
        }
    }
}
