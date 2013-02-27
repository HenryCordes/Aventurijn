using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aventurijn.Activities.Web.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Error500()
        {
            return View();
        }

        public ActionResult Error404()
        {
            var port = "";
            if (!Request.Url.IsDefaultPort)
            {
                port = string.Format(":{0}",Request.Url.Port.ToString(CultureInfo.InvariantCulture));
            }
            ViewBag.OriginalUrl = string.Format("{0}://{1}{2}{3}", Request.Url.Scheme, Request.Url.Host, port, Request.QueryString["aspxerrorpath"].ToLower(CultureInfo.InvariantCulture));
            return View();
        }

    }
}
