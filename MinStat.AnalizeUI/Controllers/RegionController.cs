﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.AnalizeUI.Filters;
using MinStat.AnalizeUI.ServiceAdapters;

namespace MinStat.AnalizeUI.Controllers
{
    [NoCache]
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
            Dictionary<string, string> federalSubjects = _infoAdapter.GetFederalSubjects(federalDistrictId).ToDictionary(x => x.Id.ToString(CultureInfo.InvariantCulture), x=> x.Title);
            return Json(federalSubjects, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Enterprises(int federalSubjectId)
        {
            Dictionary<string, string> enterprises = _infoAdapter.GetEnterprises(federalSubjectId).ToDictionary(x => x.Id.ToString(CultureInfo.InvariantCulture), x => x.Title);
            return Json(enterprises, JsonRequestBehavior.AllowGet);
        }

    }
}
