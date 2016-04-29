using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buongustaio.Controllers
{
    public class holaController : Controller
    {
        // GET: hola
        public ActionResult Index()
        {
            return View();
        }
    }
}