using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonch.Domain.Abstract;
using Bonch.Domain.POCO;
using Bonch.MinUI.Infrustructure;
using Bonch.MinUI.Models;

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
            IEnumerable<StatisticDataItem> statisticItems = new List<StatisticDataItem>();
            
            if ((!String.IsNullOrEmpty(districtId) && districtId != "0")  && 
                (String.IsNullOrEmpty(subjectId) || subjectId == "0") && 
                (String.IsNullOrEmpty(enterpriseId) || enterpriseId == "0"))
            {
                int id;
                if (Int32.TryParse(districtId, out id))
                {
                    statisticItems = _statisticRepository.GetItems(id, DateTime.Now.AddDays(-50), DateTime.Now, AreaType.District);
                }
            }
            else if (
                (!String.IsNullOrEmpty(districtId) && districtId != "0") &&
                (!String.IsNullOrEmpty(subjectId) && subjectId != "0") &&
                (String.IsNullOrEmpty(enterpriseId) || enterpriseId == "0"))
            {
                int id;
                if (Int32.TryParse(subjectId, out id))
                {
                    statisticItems = _statisticRepository.GetItems(id, DateTime.Now.AddDays(-50), DateTime.Now, AreaType.Subject);
                }
            }
            else if (
                (!String.IsNullOrEmpty(districtId) && districtId != "0") &&
                (!String.IsNullOrEmpty(subjectId) && subjectId != "0") &&
                (!String.IsNullOrEmpty(enterpriseId) && enterpriseId != "0"))
            {
                int id;
                if (Int32.TryParse(enterpriseId, out id))
                {
                    statisticItems = _statisticRepository.GetItems(id, DateTime.Now.AddDays(-50), DateTime.Now, AreaType.Enterprise);
                }
            }

            return View(statisticItems);
        }

        public ActionResult Selectors(string enterpriseId, string subjectId, string districtId)
        {
            SelectorsViewModel model = new SelectorsViewModel();
            model.Districts = _infrastructureRepository.GetFederalDistricts().ToList();
            model.Districts.Insert(0, new FederalDistrict { Id = 0, Title = String.Empty });
            model.Subjects = new List<FederalSubject>();
            model.Enterprises = new List<Enterprise>();

            int dId;
            if (Int32.TryParse(districtId, out dId))
            {
                model.Subjects = _infrastructureRepository.GetFederationSubjects(dId).ToList();
                model.Subjects.Insert(0, new FederalSubject { Id = 0, Title = String.Empty });

            }

            int sId;
            if (Int32.TryParse(subjectId, out sId))
            {
                model.Enterprises = _infrastructureRepository.GetEnterprises(sId).ToList();
                model.Enterprises.Insert(0, new Enterprise { Id = 0, Title = String.Empty });

            }
            return PartialView(model);
        }

    }
}
