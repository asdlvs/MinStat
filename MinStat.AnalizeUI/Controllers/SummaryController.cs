﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.AnalizeUI.Models;
using MinStat.AnalizeUI.ServiceAdapters;

namespace MinStat.AnalizeUI.Controllers
{
    public class SummaryController : Controller
    {
        private IInfoDataAdapter _infoAdapter;
        private SummaryReportAdapter _summaryAdapter;

        public SummaryController()
        {
            _infoAdapter = new InfoDataAdapter();
            _summaryAdapter = new SummaryReportAdapter();

            Dictionary<int, string> federalDistricts = _infoAdapter.GetFederalDistricts().ToDictionary(x => x.Key, x => x.Value);
            federalDistricts.Add(0, "Все округа");
            ViewBag.FederalDistricts = federalDistricts.OrderBy(x => x.Key);
            ViewBag.EndDate = DateTime.Now.ToShortDateString();
        }

        public ActionResult Index()
        {
            SummaryReportCreatorModel model = new SummaryReportCreatorModel();
            model.Activities = _infoAdapter.GetActivities();
            model.EducationLevels = _infoAdapter.GetEducationLevels();
            model.PostLevels = _infoAdapter.GetPostLevels();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(SummaryReportCreatorModel model)
        {
            IEnumerable<StatisticDataModel> statisticData = _summaryAdapter.GetSummaryReport(model);
            return View("StaticStatisticData", statisticData);
        }

    }
}