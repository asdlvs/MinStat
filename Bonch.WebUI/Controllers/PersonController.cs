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

    [Authorize]
    [NoCache]
    public class PersonController : Controller
    {
        //
        // GET: /Person/
        ISummariesRepository _sumRep;
        IPersonsRepository _perRep;
        public PersonController(ISummariesRepository sumRep, IPersonsRepository perRep)
        {
            _sumRep = sumRep;
            _perRep = perRep;
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult UndeliveredSummaries()
        {
            IEnumerable<SummaryViewModel> summaries = _sumRep.Undelivered().Select(x => new SummaryViewModel
            {
                Id = x.Id.ToString(),
                AuthorName = x.AuthorName,
                CreateDate = x.CreateDate.ToShortDateString(),
                Title = x.Title
            });
            return this.Json(summaries, JsonRequestBehavior.AllowGet);
        }
        public JsonResult List(SummaryViewModel summary, ActivityViewModel activity)
        {
            IEnumerable<PersonViewModel> persons = _perRep.List(
                    new Summary { Id = Int32.Parse(summary.Id) }, new Activity { Id = Int32.Parse(activity.Id) }
                )
                .Select(x => new PersonViewModel
                {
                    Id = x.Id.ToString(),
                    Age = ((DateTime.Now - x.BirthDate).Days / 365).ToString(),
                    EducationLevel = x.EducationLevelId.ToString(),
                    FirstName = x.FirstName,
                    Job = x.Job,
                    JobLevel = x.JobLevelId.ToString(),
                    LastName = x.LastName,
                    Salary = x.Salary.ToString()

                });
            return this.Json(persons, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Activities(SummaryViewModel summary)
        {
            IEnumerable<ActivityViewModel> activities = _sumRep
                .Activities(new Summary { Id = Int32.Parse(summary.Id) })
                .Where(x => x.Checked)
                .Select(x => new ActivityViewModel
            {
                Id = x.Id.ToString(),
                Title = x.Title,
                Checked = x.Checked
            });
            return this.Json(activities, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Set(PersonViewModel person, ActivityViewModel activity, SummaryViewModel summary)
        {
            if (person != null)
            {
                if (person.Id == "null")
                {
                    //TODO: BirthDate
                    _perRep.Set(new Person
                    {
                        BirthDate = DateTime.Now.AddYears(-25),
                        FirstName = person.FirstName,
                        EducationLevelId = Int32.Parse(person.EducationLevel),
                        Job = person.Job,
                        JobLevelId =Int32.Parse(person.JobLevel),
                        LastName = person.LastName,
                        Salary = Decimal.Parse(person.Salary),
                        ActivityId = Int32.Parse(activity.Id),
                        SummaryId = Int32.Parse(summary.Id)

                    });
                }
                else
                {
                    Person selectedPerson = _perRep.List(new Summary { Id = Int32.Parse(summary.Id) }, new Activity { Id = Int32.Parse(activity.Id) })
                    .Single(x => x.Id.ToString() == person.Id);
                    _perRep.Set(selectedPerson);
                }
            }
            return this.Json(person ?? new PersonViewModel());
        }
        [HttpPost]
        public JsonResult Delete(PersonViewModel person)
        {
            _perRep.Delete(new Person { Id = Int32.Parse(person.Id) });
            return this.Json(null);
        }


    }
}
