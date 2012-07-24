using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MinStat.Enterprises.WebUI.EnterpriseServiceReference;
using MinStat.Enterprises.WebUI.Models;

namespace MinStat.Enterprises.WebUI.ServiceAdapters
{
    public class EnterpriseServiceAdapter : IEnterpriseServiceAdapter
    {
        public void CreateSummary(string title, int[] activitiesIds)
        {
            using (EnterpriseDataServiceClient proxy = new EnterpriseDataServiceClient())
            {
                proxy.CreateSummary(title, activitiesIds);
            }
        }

        public IEnumerable<SummaryModel> GetSummaries()
        {
            using (EnterpriseDataServiceClient proxy = new EnterpriseDataServiceClient())
            {
                IEnumerable<Summary> summaries = proxy.GetSummaries();
                foreach (var summary in summaries)
                {
                    yield return new SummaryModel 
                    {
                        Id = summary.Idk__BackingField,
                        Title = summary.Titlek__BackingField,
                        CreateDate = summary.CreateDatek__BackingField.ToShortDateString(),
                        Published = summary.Publishedk__BackingField,
                        PublishedDate = summary.PublishedDatek__BackingField.ToShortDateString()
                    };
                }
            }
        }


        public IEnumerable<ActivityModel> GetActivities()
        {
            using (EnterpriseDataServiceClient proxy = new EnterpriseDataServiceClient())
            {
                IEnumerable<Activity> activities = proxy.GetActivities();
                foreach(Activity activity in activities)
                {
                    yield return new ActivityModel
                                     {
                                         Id = activity.Idk__BackingField,
                                         Title = activity.Titlek__BackingField,
                                         Part_1 = activity.Part_1k__BackingField,
                                         Part_2 = activity.Part_2k__BackingField,
                                         Part_3 = activity.Part_3k__BackingField,
                                         Part_4 = activity.Part_4k__BackingField,
                                         Part_5 = activity.Part_5k__BackingField
                                     };
                }
            }
        }

        public IEnumerable<ActivityModel> GetActivities(int summaryId)
        {
            throw new NotImplementedException();
        }


        public void RemoveSummary(int summaryId)
        {
            using (EnterpriseDataServiceClient proxy = new EnterpriseDataServiceClient())
            {
                proxy.RemoveSummary(summaryId);
            }
        }


        public void CopySummary(string title, int summaryId)
        {
            using (EnterpriseDataServiceClient proxy = new EnterpriseDataServiceClient())
            {
                proxy.CopySummary(title, summaryId);
            }
        }


        public void PublishSummary(int summaryId)
        {
            using (EnterpriseDataServiceClient proxy = new EnterpriseDataServiceClient())
            {
                proxy.PublishSummary(summaryId);
            }
        }
    }
}