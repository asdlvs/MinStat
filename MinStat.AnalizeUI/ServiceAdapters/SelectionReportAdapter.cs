using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MinStat.AnalizeUI.Models;
using MinStat.AnalizeUI.StatisticDataReference;

namespace MinStat.AnalizeUI.ServiceAdapters
{
    public class SelectionReportAdapter : ReportAdapter
    {
        public IEnumerable<StatisticDataModel> GetQtyStaticData(int enterpriseId, int federalSubjectId, int federalDistrictId, string startDate, string endDate, List<int> verticalChecks, List<KeyValuePair<int, int>> horizontalChecks)
        {
            using (var proxy = new StatisticDataServiceClient())
            {
                IEnumerable<StatisticData> statisticData = proxy.GetQtyStaticReport(enterpriseId, federalSubjectId, federalDistrictId, DateTime.Parse(startDate),DateTime.Parse(endDate), verticalChecks.ToArray(), horizontalChecks.ToArray());
                return Convert(statisticData);
            }
        }

        public IEnumerable<StatisticDataModel> GetQtyDynamicData(int enterpriseId, int federalSubjectId, int federalDistrictId, string startDate, string endDate, List<int> verticalChecks, List<KeyValuePair<int, int>> horizontalChecks)
        {
            using (var proxy = new StatisticDataServiceClient())
            {
                IEnumerable<StatisticData> statisticData = proxy.GetQtyDynamicReport(enterpriseId, federalSubjectId, federalDistrictId, DateTime.Parse(startDate), DateTime.Parse(endDate), verticalChecks.ToArray(), horizontalChecks.ToArray());
                return Convert(statisticData);
            }
        }
    }
}