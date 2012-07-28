using System;
using System.Collections.Generic;
using System.Linq;
using MinStat.AnalizeUI.Models;
using MinStat.AnalizeUI.StatisticDataReference;

namespace MinStat.AnalizeUI.ServiceAdapters
{
    public class ConsolidateReportAdapter : ReportAdapter
    {
        public IEnumerable<StatisticDataModel> GetStaticConsolidatedReport(ConsolidateReportCreatorModel model)
        {
            using (var proxy = new StatisticDataServiceClient())
            {
                IEnumerable<StatisticData> statisticData = proxy.GetStaticConsolidatedReport(model.EnterpriseId, model.FederalSubjectId, model.FederalDistrictId, DateTime.Parse(model.StartDate), DateTime.Parse(model.EndDate), model.SelectedActivities.ToArray(), model.SelectedCriteries.ToArray());
                return Convert(statisticData);
            }
        }

        public IEnumerable<StatisticDataModel> GetDynamicConsolidatedReport(ConsolidateReportCreatorModel model)
        {
            using (var proxy = new StatisticDataServiceClient())
            {
                IEnumerable<StatisticData> statisticData = proxy.GetDynamicConsolidatedReport(model.EnterpriseId, model.FederalSubjectId, model.FederalDistrictId, DateTime.Parse(model.StartDate), DateTime.Parse(model.EndDate), model.SelectedActivities.ToArray(), model.SelectedCriteries.ToArray());
                return Convert(statisticData);
            }
        }
    }
}