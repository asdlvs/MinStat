using System;
using System.Collections.Generic;
using MinStat.AnalizeUI.Models;
using MinStat.AnalizeUI.StatisticDataReference;

namespace MinStat.AnalizeUI.ServiceAdapters
{
    public class ConsolidateReportAdapter : ReportAdapter
    {
        public IEnumerable<StatisticDataModel> GetConsolidateReport(int enterpriseId, int federalSubjectId, int federalDistrictId, int subsectorId, DateTime startDate, DateTime endDate)
        {
            using (var proxy = new StatisticDataServiceClient())
            {
                IEnumerable<StatisticData> statisticData = proxy.GetConsolidateReport(enterpriseId, federalSubjectId, federalDistrictId, subsectorId, startDate, endDate);
                return Convert(statisticData);
            }
        }
    }
}