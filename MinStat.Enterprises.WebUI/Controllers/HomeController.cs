using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.Enterprises.WebUI.Filters;

namespace MinStat.Enterprises.WebUI.Controllers
{
    [Authorize, NoCache]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
