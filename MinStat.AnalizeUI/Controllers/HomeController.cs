using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.AnalizeUI.Filters;

namespace MinStat.AnalizeUI.Controllers
{
    [NoCache]
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
