using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonch.WebUI.Models;

namespace Bonch.WebUI.Controllers
{
  [Authorize]
  public class SummaryController : Controller
  {
    //
    // GET: /Summary/

    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public JsonResult Summaries()
    {
      IEnumerable<SummaryViewModel> summaries = new List<SummaryViewModel> 
            { 
                new SummaryViewModel{AuthorName="Vitaliy Lebedev", CreateDate=DateTime.Now.AddDays(-20).ToShortDateString(), Id="1", PersonsCount="500", Published="true", Title= "1 квартал 2010"},
                new SummaryViewModel{AuthorName="Vitaliy Lebedev", CreateDate=DateTime.Now.AddDays(-19).ToShortDateString(), Id="2", PersonsCount="500", Published="true", Title= "2 квартал 2010"},
                new SummaryViewModel{AuthorName="Vitaliy Lebedev", CreateDate=DateTime.Now.AddDays(-18).ToShortDateString(), Id="3", PersonsCount="500", Published="true", Title= "3 квартал 2010"},
                new SummaryViewModel{AuthorName="Vitaliy Lebedev", CreateDate=DateTime.Now.AddDays(-17).ToShortDateString(), Id="4", PersonsCount="500", Published="true", Title= "4 квартал 2010"},
                new SummaryViewModel{AuthorName="Vitaliy Lebedev", CreateDate=DateTime.Now.AddDays(-16).ToShortDateString(), Id="5", PersonsCount="500", Published="true", Title= "1 квартал 2011"},
                new SummaryViewModel{AuthorName="Vitaliy Lebedev", CreateDate=DateTime.Now.AddDays(-15).ToShortDateString(), Id="6", PersonsCount="500", Published="true", Title= "2 квартал 2011"},
                new SummaryViewModel{AuthorName="Vitaliy Lebedev", CreateDate=DateTime.Now.AddDays(-14).ToShortDateString(), Id="7", PersonsCount="500", Published="true", Title= "3 квартал 2011"},
                new SummaryViewModel{AuthorName="Vitaliy Lebedev", CreateDate=DateTime.Now.AddDays(-13).ToShortDateString(), Id="8", PersonsCount="500", Published="true", Title= "4 квартал 2011"},
                new SummaryViewModel{AuthorName="Vitaliy Lebedev", CreateDate=DateTime.Now.AddDays(-12).ToShortDateString(), Id="9", PersonsCount="500", Published="true", Title= "1 квартал 2012"},
                new SummaryViewModel{AuthorName="Vitaliy Lebedev", CreateDate=DateTime.Now.AddDays(-11).ToShortDateString(), Id="10", PersonsCount="500", Published="false", Title= "2 квартал 2012"}
            }.OrderByDescending(s => s.CreateDate);

      return this.Json(summaries, JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
    public JsonResult Activities()
    {
      IEnumerable<ActivityViewModel> activities = new List<ActivityViewModel> 
            {
                new ActivityViewModel{Id = "1", Title="Активность 1"},
                new ActivityViewModel{Id = "2", Title="Активность 2"},
                new ActivityViewModel{Id = "3", Title="Активность 3"},
                new ActivityViewModel{Id = "4", Title="Активность 4"},
                new ActivityViewModel{Id = "5", Title="Активность 5"},
                new ActivityViewModel{Id = "6", Title="Активность 6"},
                new ActivityViewModel{Id = "7", Title="Активность 7"},
                new ActivityViewModel{Id = "8", Title="Активность 8"},
                new ActivityViewModel{Id = "9", Title="Активность 9"},
                new ActivityViewModel{Id = "10", Title="Активность 10"}
            };
      return this.Json(activities, JsonRequestBehavior.AllowGet);
    }


    [HttpPost]
    public JsonResult Add(SummaryViewModel newSummary, IEnumerable<ActivityViewModel> activities)
    {
      newSummary.Id = "11";
      newSummary.CreateDate = DateTime.Now.ToShortDateString();

      return this.Json(newSummary);
    }
  }
}
