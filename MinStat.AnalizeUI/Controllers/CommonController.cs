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

        //ToDo: Оптимизировать получение данных об округе, субъекте и предприятии
        public ActionResult Report(int? enterpriseId, int? federalSubjectId, int? federalDistrictId, int? activityId)
        {
            IEnumerable<StatisticDataModel> statisticData = _adapter.GetFastSummaryReport(enterpriseId ?? 0, federalSubjectId ?? 0, federalDistrictId ?? 0, activityId ?? 0);

            foreach (StatisticDataModel model in statisticData)
            {
                model.MainActivity = "Связи информационных технологий и массовых коммуникаций";
                model.ReportName = "Стандартный";
                model.CreatedDateTime = String.Format("{0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
                model.Activity = activityId == null ? "Все подотрасли" : _infoAdapter.GetActivities().Single(x => x.Id == activityId).Title;
                model.FederalDistrict = federalDistrictId == null
                                            ? "Все Федеральные Округа"
                                            : _infoAdapter.GetFederalDistricts().Single(x => x.Id == federalDistrictId)
                                                  .Title;

                if (federalSubjectId == null)
                {
                    model.FederalSubject = "Все субъекты федерации";
                }
                else
                {
                    FederalSubjectModel federalSubject =
                        _infoAdapter.GetFederalSubjects(0).Single(x => x.Id == federalSubjectId);
                    model.FederalSubject = federalSubject.Title;
                    model.FederalDistrict =
                        _infoAdapter.GetFederalDistricts().Single(x => x.Id == federalSubject.FederalDistrictId).Title;
                }

                if (enterpriseId == null)
                {
                    model.Enterprise = "Все предприятия";
                }
                else
                {
                    EnterpriseModel enterprise = _infoAdapter.GetEnterprises(0).Single(x => x.Id == enterpriseId);
                    model.Enterprise = enterprise.Title;
                    FederalSubjectModel federalSubject =
                       _infoAdapter.GetFederalSubjects(0).Single(x => x.Id == enterprise.FederalSubjectId);
                    model.FederalSubject = federalSubject.Title;
                    model.FederalDistrict =
                        _infoAdapter.GetFederalDistricts().Single(x => x.Id == federalSubject.FederalDistrictId).Title;
                }
            }

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
