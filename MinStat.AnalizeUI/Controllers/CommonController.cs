using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinStat.AnalizeUI.Controllers
{
  using MinStat.AnalizeUI.Models;
  using MinStat.AnalizeUI.ServiceAdapters;

  public class CommonController : Controller
  {
    private readonly ConsolidateReportAdapter _adapter;
    private readonly IInfoDataAdapter _infoAdapter;

    public CommonController()
    {
      _adapter = new ConsolidateReportAdapter();
      _infoAdapter = new InfoDataAdapter();
    }

    public ActionResult Report(int enterpriseId, int federalSubjectId, int federalDistrictId, int activityId)
    {
      IEnumerable<StatisticDataModel> statisticData = _adapter.GetDynamicConsolidatedReport(null);
      return View("DynamicStatisticData", statisticData);
    }

  }
}
