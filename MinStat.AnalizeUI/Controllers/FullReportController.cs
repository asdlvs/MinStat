using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.AnalizeUI.Filters;
using MinStat.AnalizeUI.O;
using MinStat.AnalizeUI.ServiceAdapters;
using MinStat.AnalizeUI.Models;

namespace MinStat.AnalizeUI.Controllers
{
    [NoCache]
    public class FullReportController : Controller
    {
        //
        // GET: /FullReport/
        private readonly FullReportAdapter _adapter;
        private readonly SelectionReportAdapter _selectionReport;
        private readonly InfoDataAdapter _infoAdapter;

        public FullReportController()
        {
            _adapter = new FullReportAdapter();
            _selectionReport = new SelectionReportAdapter();
            _infoAdapter = new InfoDataAdapter();

            Dictionary<int, string> federalDistricts = _infoAdapter.GetFederalDistricts().ToDictionary(x => x.Key, x => x.Value);
            federalDistricts.Add(0, "Все округа");
            ViewBag.FederalDistricts = federalDistricts.OrderBy(x => x.Key);
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToShortDateString();
            ViewBag.EndDate = DateTime.Now.ToShortDateString();
        }

        public ActionResult Index()
        {
            IEnumerable<StatisticDataModel> model = new StatisticDataModel[0];
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int? enterpriseId, int? federalSubjectId, int? federalDistrictId, string startDate, string endDate)
        {
            Session["enterpriseId"] = enterpriseId ?? 0;
            Session["federalSubjectId"] = federalSubjectId ?? 0;
            Session["federalDistrictId"] = federalDistrictId ?? 0;
            Session["startDate"] = startDate;
            Session["endDate"] = endDate;

            IEnumerable<StatisticDataModel> model = _adapter.GetFullReport(enterpriseId ?? 0, federalSubjectId ?? 0, federalDistrictId ?? 0,
                                                              DateTime.Parse(startDate), DateTime.Parse(endDate));
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.SelectedFederalSubjectId = federalSubjectId ?? 0;
            ViewBag.SelectedEnterpriseId = enterpriseId ?? 0;
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult CustomReport(SelectionChecks selectionChecks, string reportType)
        {
            if (!selectionChecks.HorizontalChecks.Any() || !selectionChecks.VerticalChecks.Any())
            {
                ModelState.AddModelError("nochecks", "Для составления отчета необходимо указать виды деятельности и категории работников!");
                return Index((int?)Session["enterpriseId"], (int?)Session["federalSubjectId"], (int?)Session["federalDistrictId"],
                             (string)Session["startDate"], (string)Session["endDate"]);
            }

            ViewBag.RenderGraphic = true;
            IEnumerable<StatisticDataModel> model = new StatisticDataModel[0];

            if (reportType.EndsWith("#static") || String.IsNullOrWhiteSpace(reportType))
            {
                model = _selectionReport.GetQtyStaticData(
                    (int)Session["enterpriseId"],
                    (int)Session["federalSubjectId"],
                    (int)Session["federalDistrictId"],
                    (string)Session["startDate"],
                    (string)Session["endDate"],
                    selectionChecks.VerticalChecks, selectionChecks.HorizontalChecks);
                return View("StaticStatisticData", model);
            }
            if (reportType.EndsWith("#dynamic"))
            {
                model = _selectionReport.GetQtyDynamicData(
                    (int)Session["enterpriseId"],
                    (int)Session["federalSubjectId"],
                    (int)Session["federalDistrictId"],
                    (string)Session["startDate"],
                    (string)Session["endDate"],
                    selectionChecks.VerticalChecks, selectionChecks.HorizontalChecks);
                return View("DynamicStatisticData", model);
            }
            return View("Index", model);
        }

    }
}
