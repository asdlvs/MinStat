using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MinStat.AnalizeUI.Models;
using MinStat.AnalizeUI.StatisticDataReference;

namespace MinStat.AnalizeUI.ServiceAdapters
{
    public class FastSummaryReportAdapter : FullReportAdapter
    {
        public IEnumerable<StatisticDataModel> GetFastSummaryReport(int enterpriseId, int federalSubjectId, int federalDistrictId, int activityId)
        {
            using (var proxy = new StatisticDataServiceClient())
            {
                IEnumerable<StatisticData> statisticData = proxy.GetFastSummaryReport(enterpriseId, federalSubjectId, federalDistrictId, activityId);
                return Convert(statisticData);
            }
        }
    }
}