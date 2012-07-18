using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonch.Domain.Abstract;
using Bonch.Domain.POCO;
using Bonch.MinUI.Infrustructure;

namespace Bonch.MinUI.Controllers
{
    [NoCache]
    public class HomeController : Controller
    {
        IStatisticDataRepository _statisticRepository;
        IPersonsRepository _personRepository;
        IInfraStructureRepository _infrastructureRepository;

        public HomeController(IStatisticDataRepository statisticDataRepository, IPersonsRepository personRepository, IInfraStructureRepository infrastructureRepository)
        {
            _statisticRepository = statisticDataRepository;
            _personRepository = personRepository;
            _infrastructureRepository = infrastructureRepository;
        }

        public ActionResult List(string enterpriseId, string subjectId, string districtId, string beginDate, string endDate)
        {
            ViewBag.Districts = _infrastructureRepository
                .GetFederalDistricts()
                .Select(x => new SelectListItem 
                { 
                    Text = x.Title, 
                    Value = x.Id.ToString(), 
                    Selected = x.Id.ToString() == districtId
                })
                .ToList();
            IEnumerable<StatisticDataItem> statisticItems = new List<StatisticDataItem>();

            if (!String.IsNullOrWhiteSpace(districtId))
            {
                int dId = Int32.Parse(districtId);
                //statisticItems = _statisticRepository.GetItems(dId, DateTime.Now.AddDays(-100), DateTime.Now);
                ViewBag.Subjects = _infrastructureRepository.GetFederationSubjects(dId).ToList();
            }
            if (!String.IsNullOrEmpty(subjectId))
            {
                int sId = Int32.Parse(subjectId);
                ViewBag.Enterprises = _infrastructureRepository.GetEnterprises(sId).ToList();
            }

            return View(statisticItems);
        }

    }
}
