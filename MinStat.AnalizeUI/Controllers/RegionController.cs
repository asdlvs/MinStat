using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.AnalizeUI.ServiceAdapters;

namespace MinStat.AnalizeUI.Controllers
{
    public class RegionController : Controller
    {
        private IInfoDataAdapter _infoAdapter;

        public RegionController(IInfoDataAdapter infoAdapter)
        {
            _infoAdapter = infoAdapter;
        }

        public RegionController():this(new InfoDataAdapter())
        {
        }

        public JsonResult FederalSubjects(int federalDistrictId)
        {
            Dictionary<string, string> federalSubjects = _infoAdapter.GetFederalSubjects(federalDistrictId).ToDictionary(x => x.Key.ToString(CultureInfo.InvariantCulture), x=> x.Value);
            return Json(federalSubjects, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Enterprises(int federalSubjectId)
        {
            Dictionary<string, string> enterprises = _infoAdapter.GetEnterprises(federalSubjectId).ToDictionary(x => x.Key.ToString(CultureInfo.InvariantCulture), x => x.Value);
            return Json(enterprises, JsonRequestBehavior.AllowGet);
        }

    }
}
