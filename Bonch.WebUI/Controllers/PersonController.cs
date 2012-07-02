using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonch.WebUI.Models;

namespace Bonch.WebUI.Controllers
{
    public class PersonController : Controller
    {
        //
        // GET: /Person/

        public ActionResult Index()
        {
            return View();
        }

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

            return this.Json(null);
        }

    }
}
