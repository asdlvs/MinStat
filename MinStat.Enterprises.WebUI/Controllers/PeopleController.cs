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
           if(summaryId == 0)
               return View("ChooseSummary", _enterpriseAdapter.GetSummaries());

            PeopleModel model = new PeopleModel();
            model.SummaryId = summaryId;
            model.OrderBy = orderby;
            model.PageSize = Int32.Parse(ConfigurationManager.AppSettings["peoplepagesize"]);
            model.Count = _enterpriseAdapter.GetPeopleCount(summaryId);
            model.PersonModels = _enterpriseAdapter.GetPeople(summaryId, model.PageSize, (page - 1) * model.PageSize, orderby);
            return View(model);
        }

        //public ActionResult Index(int summaryId)
        //{
        //  return this.View();
        //}

    }
}
