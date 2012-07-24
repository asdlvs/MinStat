using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonch.WebUI.Models;
using Bonch.Domain.Abstract;
using Bonch.Domain.POCO;
using Bonch.WebUI.Infrustructure;
using Bonch.Security;

namespace Bonch.WebUI.Controllers
{

    [NoCache]
    [Authorize]
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
            IEnumerable<SummaryViewModel> summaries = _summaryRep.List().Select(s => new SummaryViewModel
            {
                Id = s.Id.ToString(),
                Title = s.Title,
                AuthorName = s.AuthorName,
                CreateDate = s.CreateDate.ToShortDateString(),
                PersonsCount = s.PersonsCount.ToString(),
                Published = s.Published.ToString()
            });
            return View(summaries);
        }




        [HttpGet]
        public JsonResult Activities(string Id)
        {
            int summaryId = 0;
            if (Id != null)
                Int32.TryParse(Id, out summaryId);
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
        public ActionResult Save(string id, string title, IEnumerable<ActivityViewModel> activities)
        {
            MinStatIdentity identity = (MinStatIdentity)this.HttpContext.User.Identity;
            Summary summary = null;
            List<Activity> acts = activities.Where(x => x.Checked).Select(x => new Activity { Id = Int32.Parse(x.Id) }).ToList();
            int summaryId;
            if (Int32.TryParse(id, out summaryId))
            {
                summary = _summaryRep.List().Single(x => x.Id == summaryId);
            }
            else
            {
                summary = new Summary
                {
                    Title = title,
                    CreateDate = DateTime.Now,
                    Published = false,
                    AuthorName = identity.FullName,
                    EnterpriseId = identity.EnterpriseId
                };
            }

            this._summaryRep.Save(summary, acts);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Publish(string id)
        {
            int summaryId;
            if (Int32.TryParse(id, out summaryId))
            {
                _summaryRep.Publish(summaryId);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Copy(string oldId, string newTitle)
        {
            int oldSummaryId = 0;
            if (Int32.TryParse(oldId, out oldSummaryId) && !String.IsNullOrEmpty(newTitle))
            {
                MinStatIdentity identity = (MinStatIdentity)this.HttpContext.User.Identity;
                Summary oldSummary = _summaryRep.List().Single(x => x.Id == oldSummaryId);
                Summary newSummary = new Summary
                {
                    Title = newTitle,
                    CreateDate = DateTime.Now,
                    Published = false,
                    AuthorName = identity.FullName,
                    EnterpriseId = identity.EnterpriseId
                };
                _summaryRep.Copy(oldSummary, newSummary);
            }
            return RedirectToAction("Index");
        }

        

    }
}
