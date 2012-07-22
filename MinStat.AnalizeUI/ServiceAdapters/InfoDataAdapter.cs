using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}