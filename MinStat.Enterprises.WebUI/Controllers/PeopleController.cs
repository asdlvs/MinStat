using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinStat.Enterprises.WebUI.Controllers
{
  using MinStat.Enterprises.WebUI.ServiceAdapters;
  using MinStat.Enterprises.WebUI.Models;

  [Authorize]
  public class PeopleController : Controller
  {
    private IEnterpriseServiceAdapter _enterpriseAdapter;

    public PeopleController(IEnterpriseServiceAdapter enterpriseServiceAdapter)
    {
      _enterpriseAdapter = enterpriseServiceAdapter;
    }

    public PeopleController()
      : this(new EnterpriseServiceAdapter())
    {
    }

    public ActionResult Index(int summaryId = 0, int page = 1, string orderby = "Title")
    {
      if (summaryId == 0) return RedirectToAction("Index", "Summaries");
      if (page < 1) page = 1;

      PeopleModel model = new PeopleModel();
      model.Page = page;
      model.SummaryId = summaryId;
      model.OrderBy = orderby;
      model.Direction = orderby.EndsWith("Desc", StringComparison.OrdinalIgnoreCase) ? "" : " Desc";
      model.PageSize = Int32.Parse(ConfigurationManager.AppSettings["peoplepagesize"]);
      model.Count = _enterpriseAdapter.GetPeopleCount(summaryId);
      model.PersonModels = _enterpriseAdapter.GetPeople(summaryId, model.PageSize, (page - 1) * model.PageSize, orderby);
      return View(model);
    }

    public PartialViewResult Edit(int summaryId)
    {
      PersonModel model = new PersonModel();
      model.SummaryId = summaryId;
      ViewBag.EducationLevels = _enterpriseAdapter.GetEducationLevels();
      ViewBag.PostLevels = _enterpriseAdapter.GetPostLevels();
      ViewBag.Activities = _enterpriseAdapter.GetActivities(summaryId);
      return this.PartialView(model);
    }

    [HttpPost]
    public ActionResult Edit(PersonModel model)
    {
      ViewBag.EducationLevels = _enterpriseAdapter.GetEducationLevels();
      ViewBag.PostLevels = _enterpriseAdapter.GetPostLevels();
      ViewBag.Activities = _enterpriseAdapter.GetActivities(model.SummaryId);
      if (ModelState.IsValid)
      {
        if (model.Id == 0)
        {
          _enterpriseAdapter.CreatePerson(model);
        }
        else
        {
          _enterpriseAdapter.UpdatePerson(model);
        }
      }
      return this.PartialView(model);
    }
  }
}
