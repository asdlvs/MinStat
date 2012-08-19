using System;
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

            Dictionary<int, string> federalDistricts = _infoAdapter.GetFederalDistricts().ToDictionary(x => x.Id, x => x.Title);
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
            if (!model.SelectedActivities.Any()
                || !model.SelectedGenders.Any()
                || !model.SelectedEducationLevels.Any()
                ||!model.SelectedPostLevels.Any()
                )
            {
                ModelState.AddModelError("activities", "Необходимо указать виды деятельности.");
                ModelState.AddModelError("genders", "Необходимо указать пол.");
                ModelState.AddModelError("educationlevels", "Необходимо указать уровни образования.");
                ModelState.AddModelError("postlevels", "Необходимо указать категории работников.");
            }

            if (!ModelState.IsValid) { return Index(); }

            IEnumerable<StatisticDataModel> statisticData = _summaryAdapter.GetSummaryReport(model);
            foreach (StatisticDataModel eModel in statisticData)
            {
                eModel.MainActivity = "Связи информационных технологий и массовых коммуникаций";
                eModel.ReportName = "Стандартный";
                eModel.CreatedDateTime = String.Format("{0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
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

            return View("StaticStatisticData", statisticData);
        }

    }
}
