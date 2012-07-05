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
=======
  [Authorize]
  public class PersonController : Controller
  {
    //
    // GET: /Person/
>>>>>>> c4c84d3cdc2503e968c7527b3dcc9fae6b0d1502

    public ActionResult Index()
    {
      return View();
    }

<<<<<<< HEAD
        public JsonResult UndeliveredSummaries()
        {
            IEnumerable<SummaryViewModel> summaries = _sumRep.Undelivered().Select(x => new SummaryViewModel
            {
                Id = x.Id.ToString(),
                AuthorName = x.AuthorName,
                CreateDate = x.CreateDate.ToShortDateString(),
                PersonsCount = x.PersonsCount.ToString(),
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
                    EducationLevel = x.EducationLevel,
                    FirstName = x.FirstName,
                    Job = x.Job,
                    JobLevel = x.JobLevel,
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
                //TODO: BirthDate
                _perRep.Set(new Person
                {
                    BirthDate = DateTime.Now.AddYears(-25),
                    FirstName = person.FirstName,
                    EducationLevel = person.EducationLevel,
                    Job = person.Job,
                    JobLevel = person.JobLevel,
                    LastName = person.LastName,
                    Salary = Decimal.Parse(person.Salary),
                    ActivityId = Int32.Parse(activity.Id),
                    SummaryId = Int32.Parse(summary.Id)

                });
            }
            return this.Json(person ?? new PersonViewModel());
        }

        [HttpPost]
        public JsonResult Delete(PersonViewModel person)
        {
            _perRep.Delete(new Person { Id = Int32.Parse(person.Id) });
            return this.Json(null);
        }
=======
    public JsonResult UndeliveredSummaries()
    {
      IEnumerable<SummaryViewModel> summaries = new List<SummaryViewModel> 
            {
                new SummaryViewModel{Id = "9", AuthorName = "Vitaliy Lebedev", CreateDate = DateTime.Now.ToShortDateString(), PersonsCount = "0", Published = Boolean.FalseString, Title = "1 квартал 2012"},
                new SummaryViewModel{Id = "10", AuthorName = "Vitaliy Lebedev", CreateDate = DateTime.Now.ToShortDateString(), PersonsCount = "0", Published = Boolean.FalseString, Title = "2 квартал 2012"}
            };
      return this.Json(summaries, JsonRequestBehavior.AllowGet);
    }

    public JsonResult Persons(SummaryViewModel summary, ActivityViewModel activity)
    {
      IEnumerable<PersonViewModel> persons = new List<PersonViewModel>
            {
                new PersonViewModel{Id = "1", FirstName = "Petr", LastName = "Maximov", Salary = "50000"},
                new PersonViewModel{Id = "2", FirstName = "Vasiliy", LastName = "Maximov", Salary = "50000"},
                new PersonViewModel{Id = "3", FirstName = "Alexander", LastName = "Maximov", Salary = "50000"}
            };
      return this.Json(persons, JsonRequestBehavior.AllowGet);
    }

    public JsonResult Activities(SummaryViewModel summary)
    {
      IEnumerable<ActivityViewModel> activities = new List<ActivityViewModel>()
            {
                new ActivityViewModel{Id = "1", Title = "Активность 1", Checked = "true"},
                new ActivityViewModel{Id = "2", Title = "Активность 2", Checked = "true"},
                new ActivityViewModel{Id = "3", Title = "Активность 3", Checked = "true"}
            };
      return this.Json(activities, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public JsonResult SetPerson(PersonViewModel person)
    {
>>>>>>> c4c84d3cdc2503e968c7527b3dcc9fae6b0d1502

      return this.Json(null);
    }

  }
}
