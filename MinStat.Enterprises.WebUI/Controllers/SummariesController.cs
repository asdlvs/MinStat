using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.Enterprises.WebUI.Filters;
using MinStat.Enterprises.WebUI.ServiceAdapters;
using MinStat.Enterprises.WebUI.Models;

namespace MinStat.Enterprises.WebUI.Controllers
{
    [Authorize, NoCache]
    public class SummariesController : Controller
    {
        //
        // GET: /Summaries/
        private IEnterpriseServiceAdapter _serviceAdapter;

        public SummariesController(IEnterpriseServiceAdapter serviceAdapter)
        {
            _serviceAdapter = serviceAdapter;
        }

        public SummariesController()
            : this(new EnterpriseServiceAdapter())
        {
        }

        public ActionResult Index()
        {
            IEnumerable<SummaryModel> model = _serviceAdapter.GetSummaries();
            ViewBag.Activities = _serviceAdapter.GetActivities();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SummaryModel summary)
        {
            if (!String.IsNullOrWhiteSpace(summary.Title))
            {
                if (summary.Id > 0)
                {
                    _serviceAdapter.UpdateSummary(summary.Id, summary.Title, summary.Activities);
                }
                else
                {
                    _serviceAdapter.CreateSummary(summary.Title, summary.Activities);

                }
            }
            else
            {
                ModelState.AddModelError("", "Укажите название отчета.");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Remove(int summaryId)
        {
            _serviceAdapter.RemoveSummary(summaryId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Copy(string Title, int summaryId)
        {
            _serviceAdapter.CopySummary(Title, summaryId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Publish(int summaryId)
        {
            _serviceAdapter.PublishSummary(summaryId);
            return RedirectToAction("Index");
        }

        public JsonResult Activities(int summaryId)
        {
            IEnumerable<ActivityModel> activities = _serviceAdapter.GetActivities(summaryId);
            return Json(activities, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> fileUpload, int summaryId)
        {
            foreach (var file in fileUpload)
            {
                if (file == null) continue;
                
                byte[] fileByteArray = new byte[file.InputStream.Length];
                file.InputStream.Read(fileByteArray, 0, fileByteArray.Length);
                _serviceAdapter.UploadPersons(fileByteArray, summaryId);
            }
            return RedirectToAction("Index");
        }

    }
}
