using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinStat.AnalizeUI.Controllers
{
    using Models;
    using ServiceAdapters;

    public class CommonController : Controller
    {
        private readonly FastSummaryReportAdapter _adapter;
        private IInfoDataAdapter _infoAdapter;

        public CommonController()
        {
            _adapter = new FastSummaryReportAdapter();
            _infoAdapter = new InfoDataAdapter();
        }

        public ActionResult Report(int? enterpriseId, int? federalSubjectId, int? federalDistrictId, int? activityId)
        {
            IEnumerable<StatisticDataModel> statisticData = _adapter.GetFastSummaryReport(enterpriseId ?? 0, federalSubjectId ?? 0, federalDistrictId ?? 0, activityId ?? 0);
            return View("StaticStatisticData", statisticData);
        }

        public ActionResult Chooser()
        {
            FastSummarySelectModel model = new FastSummarySelectModel();
            model.Activities = _infoAdapter.GetActivities();
            model.Enterprises = _infoAdapter.GetEnterprises(0);
            model.FederalSubjects = _infoAdapter.GetFederalSubjects(0);
            model.FederalDistricts = _infoAdapter.GetFederalDistricts();
            return PartialView(model);
        }

    }
}
