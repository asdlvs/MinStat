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
            var resultList = result.ToList();
            IEnumerable<DateTime> periods = resultList.Select(x => x.StartPeriodDate).Distinct().ToList();

            StatisticData statisticData = new StatisticData();
            statisticData.Titles = new Dictionary<string, string>();
            statisticData.Lines = new List<StatisticDataItem>();
            statisticData.Titles.Add("", "");
            foreach (DateTime period in periods)
            {
                statisticData.Titles.Add(period.ToShortDateString(), period.ToShortDateString());
            }
            var activities = resultList.Select(x => new { x.ActivityId, x.ActivityTitle, x.Part5 }).Distinct();
            
            foreach(var activity in activities)
            {
                if(activity.Part5 == 0)
                    continue;
                
                StatisticDataItem statisticDataItem = new StatisticDataItem();
                statisticDataItem.Id = activity.ActivityId.ToString(CultureInfo.InvariantCulture);
                statisticDataItem.Title = activity.ActivityTitle;
                statisticDataItem.Values = new List<string>();
                foreach(DateTime startPeriod in periods)
                {
                    decimal value = resultList.Where(x => x.StartPeriodDate == startPeriod && x.ActivityId == activity.ActivityId).Sum(x => x.PeoplesCount);
                    statisticDataItem.Values.Add(value != 0 ? value.ToString("0") : "0");
                }
                statisticData.Lines.Add(statisticDataItem);
            }

            List<int> summaryResults = new List<int>();
            foreach (StatisticDataItem dataItem in statisticData.Lines)
            {
                int i = 0;
                foreach (string value in dataItem.Values)
                {
                    if (summaryResults.Count() > i)
                    {
                        int currentSummaryValue = summaryResults[i];
                        currentSummaryValue += Int32.Parse(value);
                        summaryResults[i] = currentSummaryValue;
                    }
                    else
                    {
                        summaryResults.Add(Int32.Parse(value));
                    }
                    i++;
                }
            }
            StatisticDataItem resultDataItem = new StatisticDataItem
            {
                Id = "0-0",
                StrongLevel = 1,
                Title = "Итого: ",
                Values = summaryResults.Select(x => x.ToString()).ToList()
            };

            statisticData.Lines.Add(resultDataItem);
            return new List<StatisticData> { statisticData };
        }


        public IEnumerable<StatisticData> Convert(IEnumerable<SelectionQtyDynamicReportItem> result, List<int> criteries)
        {
            throw new NotImplementedException();
        }
    }
}
