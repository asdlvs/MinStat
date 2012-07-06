using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonch.WebUI.Models;

namespace Bonch.WebUI.Controllers
{
  using Bonch.Domain.Abstract;
  using Bonch.Domain.POCO;

  public class PeopleController : Controller
  {
    //
    // GET: /People/

    private IPersonsRepository _personsRepository;
    private ISummariesRepository _summariesRepository;
    public PeopleController(IPersonsRepository personsRepository, ISummariesRepository summariesRepository)
    {
      _personsRepository = personsRepository;
      _summariesRepository = summariesRepository;
    }

    public ActionResult List(string summaryId, string activityId)
    {
      int sId = 0;
      int aId = 0;
      IEnumerable<PersonViewModel> persons = null;
      if (Int32.TryParse(summaryId, out sId) && Int32.TryParse(activityId, out aId))
      {
        persons = _personsRepository.List(new Summary { Id = sId }, new Activity { Id = aId })
          .OrderBy(x => x.LastName)
          .ThenBy(x => x.FirstName)
          .Select(
          x =>
          new PersonViewModel
            {
              Id = x.Id.ToString(),
              FirstName = x.FirstName,
              LastName = x.LastName,
              Salary = x.Salary.ToString(),
              Age = ((DateTime.Now - x.BirthDate).Days / 365).ToString(),
              Job = x.Job
            });
      }
      ViewBag.SummaryId = summaryId;
      ViewBag.ActivityId = activityId;
      ViewBag.SummaryDropDown = this.GetUndeliviredSummaries(summaryId);
      ViewBag.ActivityDropDown = this.GetActivities(summaryId, activityId);
      return View(persons);
    }

    public ActionResult Edit(string id, string summaryId, string activityId)
    {
      PersonViewModel person = new PersonViewModel();
      int pId = 0;
      if (Int32.TryParse(id, out  pId))
      {
        Person x = _personsRepository.Get(pId);
        person = new PersonViewModel
          {
            Id = x.Id.ToString(),
            FirstName = x.FirstName,
            LastName = x.LastName,
            Salary = x.Salary.ToString(),
            Age = ((DateTime.Now - x.BirthDate).Days / 365).ToString(),
            Job = x.Job,
            ActivityId = x.ActivityId.ToString(),
            SummaryId = x.SummaryId.ToString(),
            EducationLevel = x.EducationLevelId.ToString(),
            JobLevel = x.JobLevelId.ToString(),
            Summary = x.Summary.Title,
            Activity = x.Activity.Title
          };
      }
      else
      {
        person.SummaryId = summaryId;
        person.ActivityId = activityId;
      }

      ViewBag.EducationLevels = _personsRepository.EducationLevels().Select(x => new SelectListItem
        {
          Text = x.Title, 
          Value = x.Id.ToString(),
          Selected = x.Id.ToString().Equals(person.EducationLevel)
        });
      ViewBag.JobLevels = _personsRepository.JobLevels().Select(x => new SelectListItem
      {
        Text = x.Title,
        Value = x.Id.ToString(),
        Selected = x.Id.ToString().Equals(person.JobLevel)
      }); 
      return View(person);

    }

    [HttpPost]
    public ActionResult Edit(PersonViewModel person)
    {
      if (ModelState.IsValid)
      {
        Person p = new Person
          {
            Id = Int32.Parse(person.Id ?? "0"),
            FirstName = person.FirstName,
            LastName = person.LastName,
            BirthDate = DateTime.Now.AddYears(-30),
            Job = person.Job,
            ActivityId = Int32.Parse(person.ActivityId),
            SummaryId = Int32.Parse(person.SummaryId),
            EducationLevelId = Int32.Parse(person.EducationLevel),
            Salary = Decimal.Parse(person.Salary),
            JobLevelId = Int32.Parse(person.JobLevel)
          };
        _personsRepository.Set(p);
        return this.RedirectToAction(
          "List", new { summaryId = p.SummaryId.ToString(), activityId = p.ActivityId.ToString() });
      }
      return this.View(person);
    }

    [HttpPost]
    public ActionResult Delete(string id, string summaryId, string activityId)
    {
      int pId;
      if (Int32.TryParse(id, out pId))
      {
        _personsRepository.Delete(new Person { Id = pId });
      }
      return this.RedirectToAction("List", new { summaryId, activityId });
    }

    private IEnumerable<SelectListItem> GetUndeliviredSummaries(string summaryId)
    {
      var undelivered = _summariesRepository.Undelivered().Select(x => new SelectListItem
        {
          Value = x.Id.ToString(),
          Text = x.Title,
          Selected = x.Id.ToString().Equals(summaryId)
        });
      return undelivered;
    }


    private IEnumerable<SelectListItem> GetActivities(string summaryId, string activityId = null)
    {
      int sId = 0;
      IEnumerable<SelectListItem> activities = new SelectListItem[0];
      if(Int32.TryParse(summaryId, out sId))
      {
        activities = _summariesRepository
          .Activities(new Summary { Id = sId })
          .Where(x => x.Checked)
          .Select(x => new SelectListItem
        {
          Value = x.Id.ToString(),
          Text = x.Title,
          Selected = x.Id.ToString().Equals(activityId)
        });
      }
      return activities.ToList();
    }

    public JsonResult Activities(string summaryId)
    {
      return Json(this.GetActivities(summaryId), JsonRequestBehavior.AllowGet);
    }



  }
}
