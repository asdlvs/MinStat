using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MinStat.AnalizeUI.Models;
using MinStat.AnalizeUI.StatisticDataReference;

namespace MinStat.AnalizeUI.ServiceAdapters
{
    public class InfoDataAdapter : IInfoDataAdapter
    {
        public IDictionary<int, string> GetFederalDistricts()
        {
            using (StatisticDataServiceClient proxy = new StatisticDataServiceClient())
            {
                return proxy.GetFederalDistricts();
            }
        }

        public IDictionary<int, string> GetFederalSubjects(int districtId)
        {
            using (StatisticDataServiceClient proxy = new StatisticDataServiceClient())
            {
                return proxy.GetFederalSubjects(districtId);
            }
        }

        public IDictionary<int, string> GetEnterprises(int subjectId)
        {
            using (StatisticDataServiceClient proxy = new StatisticDataServiceClient())
            {
                return proxy.GetEnterprises(subjectId);
            }
        }


        public IEnumerable<ActivityModel> GetActivities()
        {
            using (StatisticDataServiceClient proxy = new StatisticDataServiceClient())
            {
               foreach(Activity activity in proxy.GetActivities())
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

        public IEnumerable<FilterCriteryModel> GetFilterCriteries()
        {
            using (StatisticDataServiceClient proxy = new StatisticDataServiceClient())
            {
                foreach (FilterCritery critery in proxy.GetConsolidateFilterCritery())
                {
                    yield return new FilterCriteryModel
                                     {
                                         KeyValue = critery.KeyValue,
                                         Key = critery.Key,
                                         Inner = critery.Innerk__BackingField
                                     };
                }
            }
        }


        public IDictionary<int, string> GetPostLevels()
        {
            using (StatisticDataServiceClient proxy = new StatisticDataServiceClient())
            {
                return proxy.GetPostLevels();
            }
        }

        public IDictionary<int, string> GetEducationLevels()
        {
            using (StatisticDataServiceClient proxy = new StatisticDataServiceClient())
            {
                return proxy.GetEducationLevels();
            }
        }
    }
}