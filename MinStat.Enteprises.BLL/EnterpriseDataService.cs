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
using System.IO;

namespace MinStat.Enterprises.BLL
{
    [AspNetCompatibilityRequirements(RequirementsMode =
             AspNetCompatibilityRequirementsMode.Allowed)]
    [Interceptors.InterceptorBehavior]
    public class EnterpriseDataService : IEnterpriseDataService
    {
        private IIdentifier _identifier;
        private IEnterpriseDataRepository _enterpriseRepository;
        private IPersonsUploader _uploader;
        private readonly int _enterpriseId;

        public EnterpriseDataService(IIdentifier identifier, IEnterpriseDataRepository repository, IPersonsUploader uploader)
        {
            _identifier = identifier;
            _enterpriseRepository = repository;
            _uploader = uploader;
            _enterpriseId = _identifier.EnterpriseId(System.Threading.Thread.CurrentPrincipal.Identity.Name);
        }

        public EnterpriseDataService()
            : this(new EnterpriseIdentifier(), new EnterpriseDataRepository(), new PersonsUploader())
        {
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public IEnumerable<Summary> GetSummaries()
        {
            return _enterpriseRepository.GetSummaries(_enterpriseId).ToList();
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
        public void UpdatePerson(int personId, int activityId, string title, string post, int postLevelId, int educationLevelId, decimal yearSalary, bool gender, bool wasQualificationIncrease, bool wasValidate, int birthYear, int hiringYear, int startPostYear, int dismissalYear)
        {
            _enterpriseRepository.UpdatePerson(personId, activityId, title, post, postLevelId, educationLevelId, yearSalary, gender, wasQualificationIncrease, wasValidate, birthYear, hiringYear, startPostYear, dismissalYear);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public void RemovePerson(int personId)
        {
            _enterpriseRepository.RemovePerson(personId);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public IEnumerable<Activity> GetActivities()
        {
            return _enterpriseRepository.GetActivities().ToList();
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public IEnumerable<Activity> GetActivities(int summaryId)
        {
            return _enterpriseRepository.GetActivities(summaryId).ToList();
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public void PublishSummary(int summaryId)
        {
            _enterpriseRepository.PublishSummary(summaryId);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public void UpdateSummary(int summaryId, string title, List<int> activitiesIds)
        {
            _enterpriseRepository.UpdateSummary(summaryId, title, activitiesIds);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public IEnumerable<Person> GetPeoples(int summaryId, int size, int offset, string orderby)
        {
            return _enterpriseRepository.GetPeople(summaryId, size, offset, orderby).ToList();
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public int GetPeoplesArraySize(int summaryId)
        {
            return _enterpriseRepository.GetPeopleArraySize(summaryId);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public Dictionary<int, string> GetEducationLevels()
        {
            return _enterpriseRepository.GetEducationLevels().ToDictionary(x => x.Id, x => x.Title);
        }
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public Dictionary<int, string> GetPostLeves()
        {
            return _enterpriseRepository.GetPostLevels().ToDictionary(x => x.Id, x => x.Title);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public bool IsPublished(int summaryId)
        {
            return _enterpriseRepository.IsPublished(summaryId);
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public void UploadPersons(byte[] csvFile, int summaryId)
        {
            IEnumerable<Person> persons = _uploader.ParseFile(csvFile, summaryId);
            _enterpriseRepository.CreatePersons(persons);
        }
    }
}
