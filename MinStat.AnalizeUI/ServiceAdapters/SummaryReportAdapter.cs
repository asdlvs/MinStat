using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MinStat.AnalizeUI.Models;
using MinStat.AnalizeUI.StatisticDataReference;

namespace MinStat.AnalizeUI.ServiceAdapters
{
    public class SummaryReportAdapter : ReportAdapter
    {
        public IEnumerable<StatisticDataModel> GetSummaryReport(SummaryReportCreatorModel model)
        {
            using (var proxy = new StatisticDataServiceClient())
            {
                IEnumerable<StatisticData> statisticData = proxy.GetSummaryReport(model.EnterpriseId, model.FederalSubjectId,model.FederalDistrictId, DateTime.Parse(model.BoundDate),
                        model.SelectedActivities.ToArray(), model.SelectedGenders.ToArray(), model.SelectedEducationLevels.ToArray(),model.SelectedPostLevels.ToArray());
                return Convert(statisticData);
            }
        }
    }
}