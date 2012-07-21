using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.AnalizeUI.ServiceAdapters;
using MinStat.AnalizeUI.Models;

namespace MinStat.AnalizeUI.Controllers
{
    public class FullReportController : Controller
    {
        //
        // GET: /FullReport/
        private readonly FullReportAdapter _adapter;
        private IEnumerable<StatisticDataModel> _model;

        public FullReportController()
        {
            _adapter = new FullReportAdapter();
        }

        public ActionResult Index()
        {
            IEnumerable<StatisticDataModel> model = new StatisticDataModel[0];
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int enterpriseId, int federalSubjectId, int federalDistrictId, string startDate, string endDate)
        {
            _model = _adapter.GetFullReport(enterpriseId, federalSubjectId, federalDistrictId,
                                                              DateTime.Parse(startDate), DateTime.Parse(endDate));
            return View(_model);
        }

        [HttpPost]
        public ActionResult CustomReport(IEnumerable<int> b)
        {
            return View();
        }

    }
}
