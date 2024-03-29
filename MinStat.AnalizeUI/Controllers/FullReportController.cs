﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.AnalizeUI.Filters;
using MinStat.AnalizeUI.O;
using MinStat.AnalizeUI.ServiceAdapters;
using MinStat.AnalizeUI.Models;
using System.Text;

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

            Dictionary<int, string> federalDistricts = _infoAdapter.GetFederalDistricts().ToDictionary(x => x.Id, x => x.Title);
            federalDistricts.Add(0, "Все округа");
            ViewBag.FederalDistricts = federalDistricts.OrderBy(x => x.Key);
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToShortDateString();
            ViewBag.EndDate = DateTime.Now.ToShortDateString();
            ViewBag.FullReport = "active";
            ViewBag.Consolidate = "";
            ViewBag.Summary = "";
            ViewBag.Administration = "";
            ViewBag.Help = "";
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
            FillReportMetaData(model, selectionChecks.VerticalChecks, selectionChecks.HorizontalChecks);
            if (reportType.EndsWith("#static") || String.IsNullOrWhiteSpace(reportType))
            {
                Session["startDate"] = null;
                DateTime startDate = new DateTime(DateTime.Parse((string)Session["endDate"]).Year, 1, 1);
                model = _selectionReport.GetQtyStaticData(
                    (int)Session["enterpriseId"],
                    (int)Session["federalSubjectId"],
                    (int)Session["federalDistrictId"],
                    startDate.ToShortDateString(),
                    (string)Session["endDate"],
                    selectionChecks.VerticalChecks, selectionChecks.HorizontalChecks);
                FillReportMetaData(model, selectionChecks.VerticalChecks, selectionChecks.HorizontalChecks);
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
                FillReportMetaData(model, selectionChecks.VerticalChecks, selectionChecks.HorizontalChecks);
                return View("DynamicStatisticData", model);
            }
            FillReportMetaData(model, selectionChecks.VerticalChecks, selectionChecks.HorizontalChecks);
            return View("Index", model);
        }

        private void FillReportMetaData(IEnumerable<StatisticDataModel> model, List<int> selectedActivities, List<KeyValuePair<int, int>> selectedGroups)
        {
            foreach (StatisticDataModel eModel in model)
            {
                eModel.MainActivity = "Связи, информационных технологий и массовых коммуникаций";
                eModel.ReportName = "Базовый";
                eModel.CreatedDateTime = String.Format("{0} {1}", DateTime.Now.ToShortDateString(),
                                                       DateTime.Now.ToShortTimeString());

                var activityTitles = _infoAdapter.GetActivities()
                   .Where(x => selectedActivities.Contains(x.Id) && x.Part_5 != 0)
                   .Select(x => x.Title);

                var educationLevels = _infoAdapter.GetEducationLevels();
                var postLevels = _infoAdapter.GetPostLevels();

                StringBuilder groupsTitlesStringBuilder = new StringBuilder();
                foreach (KeyValuePair<int, int> group in selectedGroups)
                {
                    groupsTitlesStringBuilder.AppendFormat("<div style=\"padding-left:20px;\">{0}_{1}</div>", 
                        postLevels.First(x => x.Key == group.Key).Value, 
                        educationLevels.First(x => x.Key == group.Value).Value);
                }

                eModel.Categories = groupsTitlesStringBuilder.ToString();

                StringBuilder activityTitlesStringBuilder = new StringBuilder();

                foreach (var activityTitle in activityTitles)
                {
                    activityTitlesStringBuilder.AppendFormat("<div style=\"padding-left:20px;\">{0}</div>", activityTitle);
                }

                eModel.Activity = activityTitlesStringBuilder.ToString();

                eModel.StartDate = (string) Session["startDate"];
                eModel.EndDate = (string)Session["endDate"];
                eModel.FederalDistrict = (int) Session["federalDistrictId"] == 0
                                             ? "Все Федеральные округа"
                                             : _infoAdapter.GetFederalDistricts().Single(
                                                 x => x.Id == (int) Session["federalDistrictId"])
                                                   .Title;

                if ((int) Session["federalSubjectId"] == 0)
                {
                    eModel.FederalSubject = "Все субъекты федерации";
                }
                else
                {
                    FederalSubjectModel federalSubject =
                        _infoAdapter.GetFederalSubjects(0).Single(x => x.Id == (int) Session["federalSubjectId"]);
                    eModel.FederalSubject = federalSubject.Title;
                    eModel.FederalDistrict =
                        _infoAdapter.GetFederalDistricts().Single(x => x.Id == federalSubject.FederalDistrictId).Title;
                }

                if ((int) Session["enterpriseId"] == 0)
                {
                    eModel.Enterprise = "Все предприятия";
                }
                else
                {
                    EnterpriseModel enterprise =
                        _infoAdapter.GetEnterprises(0).Single(x => x.Id == (int) Session["enterpriseId"]);
                    eModel.Enterprise = enterprise.Title;
                    FederalSubjectModel federalSubject =
                        _infoAdapter.GetFederalSubjects(0).Single(x => x.Id == enterprise.FederalSubjectId);
                    eModel.FederalSubject = federalSubject.Title;
                    eModel.FederalDistrict =
                        _infoAdapter.GetFederalDistricts().Single(x => x.Id == federalSubject.FederalDistrictId).Title;
                }
            }
        }
    }
}
