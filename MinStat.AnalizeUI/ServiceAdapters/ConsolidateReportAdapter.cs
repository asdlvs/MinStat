using System;
using System.Collections.Generic;
using System.Linq;
using MinStat.AnalizeUI.Models;
using MinStat.AnalizeUI.StatisticDataReference;

namespace MinStat.AnalizeUI.ServiceAdapters
{
    public class ConsolidateReportAdapter : ReportAdapter
    {
        public IEnumerable<StatisticDataModel> GetConsolidateReport(ConsolidateReportCreatorModel model)
        {
            using (var proxy = new StatisticDataServiceClient())
            {
                IEnumerable<StatisticData> statisticData = proxy.GetConsolidateReport(model.EnterpriseId, model.FederalSubjectId, model.FederalDistrictId, DateTime.Parse(model.StartDate), DateTime.Parse(model.EndDate), model.SelectedActivities.ToArray(), model.SelectedCriteries.ToArray());
                return Convert(statisticData);
            }
        }
    }
}