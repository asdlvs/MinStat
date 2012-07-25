// -----------------------------------------------------------------------
// <copyright file="IEnterpriseDataService.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using MinStat.Enterprises.DAL.POCO;

namespace MinStat.Enterprises.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [ServiceContract(Name = "EnterpriseDataService")]
    public interface IEnterpriseDataService
    {
        [OperationContract]
        IEnumerable<Summary> GetSummaries();

        [OperationContract]
        void CreateSummary(string title, List<int> activitiesIds);

        [OperationContract]
        void CopySummary(string title, int summaryId);

        [OperationContract]
        void RemoveSummary(int summaryId);

        [OperationContract]
        void UpdateSummary(int summaryId, string title, List<int> activitiesIds);

        [OperationContract]
        void AddActivities(int summaryId, List<int> activitiesIds);

        [OperationContract]
        void RemoveActivity(int summaryId, int activitiId);

        [OperationContract]
        int GetPeoplesArraySize(int summaryId);

        [OperationContract]
        IEnumerable<Person> GetPeoples(int summaryId, int size, int offset, string orderby);

        [OperationContract]
        void CreatePerson(int summaryId, int activityId, string title, string post, int postLevelId, int educationLevelId, decimal yearSalary, bool gender, bool wasQualificationIncrease,
          bool wasValidate, int birthYear, int hiringYear, int startPostYear, int dismissalYear);

        [OperationContract]
        void UpdatePerson(int personId, int activityId, string title, string post, int postLevelId, int educationLevelId, decimal yearSalary, bool gender, bool wasQualificationIncrease,
          bool wasValidate, int birthYear, int hiringYear, int startPostYear, int dismissalYear);

        [OperationContract]
        void RemovePerson(int personId);

        [OperationContract]
        IEnumerable<Activity> GetActivities();

        [OperationContract(Name = "GetActivitiesBySummary")]
        IEnumerable<Activity> GetActivities(int summaryId);

        [OperationContract]
        void PublishSummary(int summaryId);

      [OperationContract]
      Dictionary<int, string> GetEducationLevels();

      [OperationContract]
      Dictionary<int, string> GetPostLeves();

      //List<Person> FindPerson(string searchText);
    }
}
