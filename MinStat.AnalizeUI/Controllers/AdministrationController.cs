using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.AnalizeUI.Filters;
using MinStat.AnalizeUI.Models;
using MinStat.AnalizeUI.ServiceAdapters;
using System.Text.RegularExpressions;

namespace MinStat.AnalizeUI.Controllers
{
    [NoCache]
    public class AdministrationController : Controller
    {
        private IAdministrationServiceAdapter _adapter;
        private IInfoDataAdapter _infoAdapter;

        public AdministrationController(IAdministrationServiceAdapter adapter, InfoDataAdapter infoDataAdapter)
        {
            _adapter = adapter;
            _infoAdapter = infoDataAdapter;

            Dictionary<int, string> federalDistricts = _infoAdapter.GetFederalDistricts().ToDictionary(x => x.Id, x => x.Title);
            federalDistricts.Add(0, "");
            ViewBag.FederalDistricts = federalDistricts.OrderBy(x => x.Key);
            ViewBag.FullReport = "";
            ViewBag.Consolidate = "";
            ViewBag.Summary = "";
            ViewBag.Help = "";
            ViewBag.Administration = "active";
        }

        public AdministrationController()
            : this(new AdministrationServiceAdapter(), new InfoDataAdapter())
        {
        }

        public ActionResult Index(int? federalSubjectId, int? federalDistrictId)
        {
            CreateEnterpriseModel model = new CreateEnterpriseModel();

            if (federalSubjectId != null && federalSubjectId != 0)
            {
                model.FederalSubjectId = (int) federalSubjectId;
                IEnumerable<EnterpriseModel> enterprises = _infoAdapter.GetEnterprises(model.FederalSubjectId);
                ViewBag.SelectedFederalSubjectId = model.FederalSubjectId;
                ViewBag.SelectedFederalDistrictId = federalDistrictId;
                model.Enterprises = enterprises.Select(x => new EnterpriseModel { Id = x.Id, Title = x.Title });
            }
            else
            {
                model.Enterprises = new EnterpriseModel[0];
            }
            return View(model);
        }

        //[HttpPost]
        //public ActionResult Create(string title, int federalSubjectId, int federalDistrictId, string mail)
        //{
        //    _adapter.CreateEnterpise(title, federalSubjectId, mail);
        //    return RedirectToAction("Index", new { federalSubjectId, federalDistrictId });
        //}

        public ActionResult Create(NewEnterpriseModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Title)) { throw new ArgumentNullException("Title"); }
            Regex regex = new Regex("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$");
            if (!regex.IsMatch(model.Mail)) { throw new ArgumentException("Mail"); }

            _adapter.CreateEnterpise(model.Title, model.FederalSubjectId, model.Mail);
            return RedirectToAction("Index", new { model.FederalSubjectId, model.FederalDistrictId });
        }

        [HttpPost]
        public ActionResult Remove(int enterpriseId, int federalSubjectId, int federalDistrictId)
        {
            _adapter.RemoveEnterprise(enterpriseId);
            return RedirectToAction("Index", new { federalSubjectId, federalDistrictId });
        }

    }
}
