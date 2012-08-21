// -----------------------------------------------------------------------
// <copyright file="IEnterpriseDataRepository.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MinStat.Enterprises.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MinStat.Enterprises.DAL.POCO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IEnterpriseDataRepository
    {
        IEnumerable<Summary> GetSummaries(int enterpriseId);

        IEnumerable<Activity> GetActivities();

        IEnumerable<Activity> GetActivities(int summaryId);

        void CreateSummary(int enterpriseId, string title, List<int> activitiesIds);

        void UpdateSummary(int summaryId, string title, List<int> activities);

        void CopySummary(int enterpriseId, string title, int summaryId);

        void RemoveSummary(int summaryId);

        void PublishSummary(int summaryId);

        void AddActivities(int summaryId, List<int> activitiesIds);

        void RemoveActivity(int summaryId, int activitiId);

        IEnumerable<Person> GetPeople(int summaryId, int size, int offset, string orderby);

        int GetPeopleArraySize(int summaryId);

        void CreatePerson(int summaryId,int activityId,string title,string post,int postLevelId,int educationLevelId,decimal yearSalary,bool gender,bool wasQualificationIncrease,
          bool wasValidate,int birthYear,int hiringYear,int startPostYear,int dismissalYear);

        void UpdatePerson(int personId, int activityId, string title, string post, int postLevelId, int educationLevelId, decimal yearSalary, bool gender, bool wasQualificationIncrease,
          bool wasValidate, int birthYear, int hiringYear, int startPostYear, int dismissalYear);

        void CreatePersons(IEnumerable<Person> persons);

        void RemovePerson(int personId);

        IEnumerable<Person> FindPerson(string searchText);

        IEnumerable<EducationLevel> GetEducationLevels();

        IEnumerable<PostLevel> GetPostLevels();

        bool IsPublished(int summaryId);

        Enterprise GetEnteprise(int enterpriseId);

    }
}
