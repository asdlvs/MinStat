using System;
using System.Collections.Generic;
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

    public ActionResult Index(int summaryId, int page = 0)
    {
      IEnumerable<PersonModel> persons = _enterpriseAdapter.GetPeople(summaryId, 20, page * 20);
      return View(persons);
    }

    //public ActionResult Index(int summaryId)
    //{
    //  return this.View();
    //}

  }
}
