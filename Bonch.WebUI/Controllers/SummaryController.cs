using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonch.WebUI.Models;
using Bonch.Domain.Abstract;
using Bonch.Domain.POCO;
using Bonch.WebUI.Infrustructure;

namespace Bonch.WebUI.Controllers
{
<<<<<<< HEAD
    [NoCache]
    public class SummaryController : Controller
    {
        //
        // GET: /Summary/
        ISummariesRepository _summaryRep;
        public SummaryController(ISummariesRepository summaryRep)
        {
            _summaryRep = summaryRep;
        }
=======
  [Authorize]
  public class SummaryController : Controller
  {
    //
    // GET: /Summary/
>>>>>>> c4c84d3cdc2503e968c7527b3dcc9fae6b0d1502

    public ActionResult Index()
    {
      return View();
    }

<<<<<<< HEAD
        [HttpGet]
        public JsonResult Summaries()
        {
            var summaries = _summaryRep.List().Select(s => new SummaryViewModel
            {
                Id = s.Id.ToString(),
                Title = s.Title,
                AuthorName = s.AuthorName,
                CreateDate = s.CreateDate.ToShortDateString(),
                PersonsCount = s.PersonsCount.ToString(),
                Published = s.Published.ToString()
            });
            return this.Json(summaries, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Activities(SummaryViewModel summary)
        {
            int summaryId = 0;
            if (summary != null)
                Int32.TryParse(summary.Id, out summaryId);
            var activities = _summaryRep.Activities(new Summary
            {
                Id = summaryId
            }).Select(a => new ActivityViewModel
            {
                Id = a.Id.ToString(),
                Title = a.Title,
                Checked = a.Checked
            });
            return this.Json(activities, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Save(SummaryViewModel newSummary, IEnumerable<ActivityViewModel> activities)
        {
            Summary summary = null;
            List<Activity> acts = activities.Where(x => x.Checked).Select(x => new Activity {Id = Int32.Parse(x.Id)}).ToList();
            int summaryId;
            if (Int32.TryParse(newSummary.Id, out summaryId))
            {
                summary = new Summary
                {
                    Id = summaryId,
                    Title = newSummary.Title,
                    AuthorName = newSummary.AuthorName,
                    CreateDate = DateTime.Parse(newSummary.CreateDate)
                };
            }
            else
            {
                summary = new Summary
                {
                    Title = newSummary.Title,
                    CreateDate = DateTime.Now,
                    Published = false,
                    AuthorName = String.Empty
                };
            }

            summary = _summaryRep.Save(summary, acts);
            return this.Json(new SummaryViewModel 
            {
                Id = summary.Id.ToString(),
                Title = summary.Title,
                AuthorName = summary.AuthorName,
                CreateDate = summary.CreateDate.ToShortDateString(),
                PersonsCount = summary.PersonsCount.ToString(),
                Published = summary.Published.ToString()
            });
        }
=======
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
>>>>>>> c4c84d3cdc2503e968c7527b3dcc9fae6b0d1502
    }
  }
}
