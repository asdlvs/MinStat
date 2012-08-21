using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinStat.AnalizeUI.Controllers
{
    public class HelpController : Controller
    {
        public HelpController()
        {
            ViewBag.FullReport = "";
            ViewBag.Consolidate = "";
            ViewBag.Summary = "";
            ViewBag.Administration = "";
            ViewBag.Help = "active";
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
