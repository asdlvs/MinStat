using System;
using System.Collections.Generic;
using MinStat.AnalizeUI.Models;
using MinStat.AnalizeUI.StatisticDataReference;

namespace MinStat.AnalizeUI.ServiceAdapters
{
    public class FullReportAdapter : ReportAdapter
    {
        public IEnumerable<StatisticDataModel> GetFullReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate)
        {
            using(var proxy = new StatisticDataServiceClient())
            {
                IEnumerable<StatisticData> statisticData = proxy.GetFullReport(enterpriseId, federalSubjectId, federalDistrictId, startDate, endDate);
                return Convert(statisticData);
            }
        }
    }
}