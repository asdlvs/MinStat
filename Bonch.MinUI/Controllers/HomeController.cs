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

        public HomeController(IStatisticDataRepository statisticDataRepository, IPersonsRepository personRepository)
        {
            _statisticRepository = statisticDataRepository;
            _personRepository = personRepository;
        }

        public ActionResult List(int summaryId = 51)
        {
            List<EnterpriseStatisticDataItem> statisticData = _statisticRepository.GetItems().Where(x => x.SummaryId == summaryId).ToList();
            List<Activity> activities = _statisticRepository.GetActivities().ToList();
            var ss = activities
                .GroupJoin(statisticData, x => x.Id, x => x.ActivityId, (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new { x, y }).ToList();

           
            return View();
        }

    }
}
