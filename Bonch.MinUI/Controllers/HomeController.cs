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

        public HomeController(IStatisticDataRepository statisticDataRepository)
        {
            _statisticRepository = statisticDataRepository;
        }

        public ActionResult List()
        {
            IEnumerable<StatisticDataItem> statisticData = _statisticRepository.GetItems();
            return View(statisticData);
        }

    }
}
