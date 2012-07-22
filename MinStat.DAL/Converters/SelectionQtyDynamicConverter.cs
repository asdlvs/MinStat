using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MinStat.DAL.POCO.ReportItems;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL.Converters
{
    public class SelectionQtyDynamicConverter : IStatisticDataConverter<SelectionQtyDynamicReportItem>
    {
        public IEnumerable<StatisticData> Convert(IEnumerable<SelectionQtyDynamicReportItem> result)
        {
            IEnumerable<DateTime> periods = result.Select(x => x.StartPeriodDate).Distinct();

            StatisticData statisticData = new StatisticData();
            statisticData.Titles = new Dictionary<string, string>();
            statisticData.Lines = new List<StatisticDataItem>();
            statisticData.Titles.Add("", "");
            foreach (DateTime period in periods)
            {
                statisticData.Titles.Add(period.ToShortDateString(), period.ToShortDateString());
            }
            var activities = result.Select(x => new {x.ActivityId, x.ActivityTitle }).Distinct();
            
            foreach(var activity in activities)
            {
                StatisticDataItem statisticDataItem = new StatisticDataItem();
                statisticDataItem.Id = activity.ActivityId.ToString(CultureInfo.InvariantCulture);
                statisticDataItem.Title = activity.ActivityTitle;
                statisticDataItem.Values = new List<string>();
                foreach(DateTime startPeriod in periods)
                {
                    int value = result.Where(x => x.StartPeriodDate == startPeriod && x.ActivityId == activity.ActivityId).Sum(x => x.PeoplesCount);
                    statisticDataItem.Values.Add(value.ToString(CultureInfo.InvariantCulture));
                }
                statisticData.Lines.Add(statisticDataItem);
            }
            return new List<StatisticData> { statisticData };
        }
    }
}
