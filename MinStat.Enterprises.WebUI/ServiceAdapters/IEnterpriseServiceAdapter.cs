using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinStat.Enterprises.WebUI.EnterpriseServiceReference;
using MinStat.Enterprises.WebUI.Models;

namespace MinStat.Enterprises.WebUI.ServiceAdapters
{
    public interface IEnterpriseServiceAdapter
    {
        IEnumerable<SummaryModel> GetSummaries();

        IEnumerable<ActivityModel> GetActivities();

        IEnumerable<ActivityModel> GetActivities(int summaryId); 

        void CreateSummary(string title, int[] activitiesIds);

        void RemoveSummary(int summaryId);

        void CopySummary(string title, int summaryId);

        void PublishSummary(int summaryId);

      void UpdateSummary(int summaryId, string title, int[] activitiesIds);

      int GetPeopleCount(int summaryId);

      IEnumerable<PersonModel> GetPeople(int summaryId, int pagesize, int offset);


    }
}
