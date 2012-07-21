using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.AnalizeUI.Models;
using MinStat.AnalizeUI.ServiceAdapters;

namespace MinStat.AnalizeUI.Controllers
{
    public class ConsolidateReportController : Controller
    {
        private readonly ConsolidateReportAdapter _adapter;

        public ConsolidateReportController()
        {
            _adapter = new ConsolidateReportAdapter();
        }

        public ActionResult Index()
        {
            IEnumerable<StatisticDataModel> model = new StatisticDataModel[0];
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int enterpriseId, int federalSubjectId, int federalDistrictId, int subsectorId, string startDate, string endDate)
        {
            IEnumerable<StatisticDataModel> model = _adapter.GetConsolidateReport(enterpriseId, federalSubjectId, federalDistrictId, subsectorId,
                                                              DateTime.Parse(startDate), DateTime.Parse(endDate));
            return View(model);
        }

    }
}
