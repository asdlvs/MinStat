using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.Text;
using MinStat.Enterprises.DAL;
using MinStat.Enterprises.DAL.POCO;

namespace MinStat.Enterprises.BLL
{
    [AspNetCompatibilityRequirements(RequirementsMode =
             AspNetCompatibilityRequirementsMode.Allowed)]
    [Interceptors.InterceptorBehavior]
    public class EnterpriseDataService : IEnterpriseDataService
    {
        private IIdentifier _identifier;
        private IEnterpriseDataRepository _enterpriseRepository;
        private readonly int _enterpriseId;

        public EnterpriseDataService(IIdentifier identifier, IEnterpriseDataRepository repository)
        {
            _identifier = identifier;
            _enterpriseRepository = repository;
            _enterpriseId = _identifier.EnterpriseId(System.Threading.Thread.CurrentPrincipal.Identity.Name);
        }

        public EnterpriseDataService()
            : this(new EnterpriseIdentifier(), new EnterpriseDataRepository())
        {
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public IEnumerable<Summary> GetSummaries()
        {
            var result = _enterpriseRepository.GetSummaries(_enterpriseId).ToList();
            return result;
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public void CreateSummary(string title, List<int> activitiesIds)
        {
            _enterpriseRepository.CreateSummary(_enterpriseId, title, activitiesIds);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public void CopySummary(string title, int summaryId)
        {
            _enterpriseRepository.CopySummary(_enterpriseId, title, summaryId);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public void RemoveSummary(int summaryId)
        {
            _enterpriseRepository.RemoveSummary(summaryId);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public void AddActivities(int summaryId, List<int> activitiesIds)
        {
            _enterpriseRepository.AddActivities(summaryId, activitiesIds);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public void RemoveActivity(int summaryId, int activitiId)
        {
            _enterpriseRepository.RemoveActivity(summaryId, activitiId);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public void CreatePerson(int summaryId, int activityId, string title, string post, int postLevelId, int educationLevelId, decimal yearSalary, bool gender, bool wasQualificationIncrease, bool wasValidate, int birthYear, int hiringYear, int startPostYear, int dismissalYear)
        {
            _enterpriseRepository.CreatePerson(summaryId, activityId, title, post, postLevelId, educationLevelId, yearSalary, gender, wasQualificationIncrease, wasValidate, birthYear, hiringYear, startPostYear, dismissalYear);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public void UpdatePerson(int personId, string title, string post, int postLevelId, int educationLevelId, decimal yearSalary, bool gender, bool wasQualificationIncrease, bool wasValidate, int birthYear, int hiringYear, int startPostYear, int dismissalYear)
        {
            _enterpriseRepository.UpdatePerson(personId, title, post, postLevelId, educationLevelId, yearSalary, gender, wasQualificationIncrease, wasValidate, birthYear, hiringYear, startPostYear, dismissalYear);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public void RemovePerson(int personId)
        {
            _enterpriseRepository.RemovePerson(personId);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public IEnumerable<Activity> GetActivities()
        {
            return _enterpriseRepository.GetActivities();
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public IEnumerable<Activity> GetActivities(int summaryId)
        {
            return _enterpriseRepository.GetActivities(summaryId);
        }


        public void PublishSummary(int summaryId)
        {
            _enterpriseRepository.PublishSummary(summaryId);
        }
    }
}
