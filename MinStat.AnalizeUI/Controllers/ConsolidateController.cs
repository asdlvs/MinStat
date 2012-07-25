using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.AnalizeUI.Models;
using MinStat.AnalizeUI.ServiceAdapters;

namespace MinStat.AnalizeUI.Controllers
{
    public class ConsolidateController : Controller
    {
        private readonly ConsolidateReportAdapter _adapter;
        private readonly IInfoDataAdapter _infoAdapter;

        public ConsolidateController()
        {
            _adapter = new ConsolidateReportAdapter();
            _infoAdapter = new InfoDataAdapter();

            Dictionary<int, string> federalDistricts = _infoAdapter.GetFederalDistricts().ToDictionary(x => x.Key, x => x.Value);
            federalDistricts.Add(0, "");
            ViewBag.FederalDistricts = federalDistricts.OrderBy(x => x.Key);
        }

        public ActionResult Index()
        {
            ConsolidateReportCreatorModel model = new ConsolidateReportCreatorModel();
            model.Activities = _infoAdapter.GetActivities();
            model.Criteries = _infoAdapter.GetFilterCriteries();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ConsolidateReportCreatorModel model)
        {
            ViewBag.SelectedFederalSubjectId = model.FederalSubjectId;
            ViewBag.SelectedEnterpriseId = model.EnterpriseId;
            return View(model);
        }

        public ActionResult Report()
        {
            IEnumerable<StatisticDataModel> model = new StatisticDataModel[0];
            return View(model);
        }

    }
}
