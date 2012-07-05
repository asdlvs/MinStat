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

        public ActionResult Index()
        {
            return View();
        }

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
    }
}
