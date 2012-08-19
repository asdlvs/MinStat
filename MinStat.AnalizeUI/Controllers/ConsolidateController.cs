using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.AnalizeUI.Filters;
using MinStat.AnalizeUI.Models;
using MinStat.AnalizeUI.ServiceAdapters;

namespace MinStat.AnalizeUI.Controllers
{
    [NoCache]
    public class ConsolidateController : Controller
    {
        private readonly ConsolidateReportAdapter _adapter;
        private readonly IInfoDataAdapter _infoAdapter;

        public ConsolidateController()
        {
            _adapter = new ConsolidateReportAdapter();
            _infoAdapter = new InfoDataAdapter();

            Dictionary<int, string> federalDistricts = _infoAdapter.GetFederalDistricts().ToDictionary(x => x.Id, x => x.Title);
            federalDistricts.Add(0, "Все округа");
            ViewBag.FederalDistricts = federalDistricts.OrderBy(x => x.Key);
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToShortDateString();
            ViewBag.EndDate = DateTime.Now.ToShortDateString();
        }

        public ActionResult Index()
        {
            ConsolidateReportCreatorModel model = new ConsolidateReportCreatorModel();
            model.Activities = _infoAdapter.GetActivities();
            model.Criteries = _infoAdapter.GetFilterCriteries();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ConsolidateReportCreatorModel model, string report)
        {
            if (report.Equals("Статический отчет"))
            {
                if(!model.SelectedActivities.Any())
                {
                    ModelState.AddModelError("noactivities", "Для создания статического отчета, необходимо указать виды деятельности!");
                    return Index();
                }
                IEnumerable<StatisticDataModel> statisticData = _adapter.GetStaticConsolidatedReport(model);
                FillReportMetaData(model, statisticData);
                return View("StaticStatisticData", statisticData);
            }
            else
            {
                if (!model.SelectedActivities.Any() || !model.SelectedCriteries.Any())
                {
                    ModelState.AddModelError("nocriteries", "Для создания статического отчета, необходимо указать виды деятельности и критерии фильтрации!");
                    return Index();
                }
                ViewBag.RenderGraphic = true;
                IEnumerable<StatisticDataModel> statisticData = _adapter.GetDynamicConsolidatedReport(model);
                FillReportMetaData(model, statisticData);
                return View("DynamicStatisticData", statisticData);
            }
        }

        private void FillReportMetaData(ConsolidateReportCreatorModel model, IEnumerable<StatisticDataModel> statisticData)
        {
            foreach (StatisticDataModel eModel in statisticData)
            {
                eModel.MainActivity = "Связи информационных технологий и массовых коммуникаций";
                eModel.ReportName = "Стандартный";
                eModel.CreatedDateTime = String.Format("{0} {1}", DateTime.Now.ToShortDateString(),
                                                       DateTime.Now.ToShortTimeString());
                eModel.StartDate = model.StartDate;
                eModel.EndDate = model.EndDate;
                eModel.FederalDistrict = model.FederalDistrictId == 0
                                             ? "Все Федеральные Округа"
                                             : _infoAdapter.GetFederalDistricts().Single(x => x.Id == model.FederalDistrictId)
                                                   .Title;

                if (model.FederalSubjectId == 0)
                {
                    eModel.FederalSubject = "Все субъекты федерации";
                }
                else
                {
                    FederalSubjectModel federalSubject =
                        _infoAdapter.GetFederalSubjects(0).Single(x => x.Id == model.FederalSubjectId);
                    eModel.FederalSubject = federalSubject.Title;
                }

                if (model.EnterpriseId == 0)
                {
                    eModel.Enterprise = "Все предприятия";
                }
                else
                {
                    EnterpriseModel enterprise = _infoAdapter.GetEnterprises(0).Single(x => x.Id == model.EnterpriseId);
                    eModel.Enterprise = enterprise.Title;
                }
            }
        }

        public ActionResult Report()
        {
            IEnumerable<StatisticDataModel> model = new StatisticDataModel[0];
            return View(model);
        }

    }
}
