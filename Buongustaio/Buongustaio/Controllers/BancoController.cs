using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buongustaio.Controllers
{
    public class BancoController : Controller
    {
        // GET: Banco/PagosGUI
        public ActionResult PagoGUI()
        {
            return View();
        }
    }
}