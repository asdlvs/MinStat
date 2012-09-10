using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MinStat.DAL.HardCode;
using MinStat.DAL.POCO;
using MinStat.DAL.POCO.ReportItems;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL.Converters
{
    public class ConsolidatedDynamicReportConverter : IStatisticDataConverter<ConsolidatedDynamicReportItem>
    {

        public IEnumerable<POCO.ResultItems.StatisticData> Convert(IEnumerable<ConsolidatedDynamicReportItem> result)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<StatisticData> Convert(IEnumerable<ConsolidatedDynamicReportItem> result, List<int> criteries)
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

            foreach (var id in criteries)
            {
                var filterItem = GetCurrentFilter(id);
                StatisticDataItem dataItem = new StatisticDataItem();
                dataItem.Id = Guid.NewGuid().ToString().Replace("-","");
                dataItem.Title = filterItem.Value;
                dataItem.Values = new List<string>();
                foreach (DateTime startPeriod in periods)
                {
                    var periodItems = resultList.Where(x => x.StartPeriodDate == startPeriod);
                    string value = "0";
                    bool isCountable = true;
                    switch (id)
                    {
                        case 1:
                            value = periodItems.Sum(x => x.All).ToString();
                            break;
                        case 2:
                            value = periodItems.Where(x => x.Gender == 1).Sum(x => x.All).ToString();
                            break;
                        case 3:
                            value = periodItems.Where(x => x.Gender == 0).Sum(x => x.All).ToString();
                            break;
                        case 4:
                            value = periodItems.Sum(x => x.Hire).ToString();
                            break;
                        case 5:
                            value = periodItems.Sum(x => x.Dismiss).ToString();
                            break;
                        case 6:
                            value = periodItems.Sum(x => x.QualificationIncrease).ToString();
                            break;
                        case 7:
                            value = periodItems.Sum(x => x.Validate).ToString();
                            break;
                        case 8:
                            value = periodItems.Sum(x => x.All).ToString();
                            break;
                        case 9:
                            value = periodItems.Where(x => x.EducationLevelId == 3).Sum(x => x.All).ToString();
                            break;
                        case 10:
                            value = periodItems.Where(x => x.EducationLevelId == 2).Sum(x => x.All).ToString();
                            break;
                        case 11:
                            value = periodItems.Where(x => x.EducationLevelId == 1).Sum(x => x.All).ToString();
                            break;
                        case 12:
                            value = periodItems.Sum(x => x.All).ToString();
                            break;
                        case 13:
                            value = periodItems.Where(x => x.PostLevelId == 1).Sum(x => x.All).ToString();
                            break;
                        case 14:
                            value = periodItems.Where(x => x.PostLevelId == 2).Sum(x => x.All).ToString();
                            break;
                        case 15:
                            value = periodItems.Where(x => x.PostLevelId == 3).Sum(x => x.All).ToString();
                            break;
                        case 16:
                            value = periodItems.Where(x => x.PostLevelId == 4).Sum(x => x.All).ToString();
                            break;
                        case 17:
                            value = periodItems.Average(x => x.MiddleAge).ToString("0.0");
                            isCountable = false;
                            break;
                        case 18:
                            value = periodItems.Average(x => x.MiddleSalary).ToString("0");
                            isCountable = false;
                            break;
                    }
                    dataItem.IsCountable = isCountable;
                    dataItem.Values.Add(value != "0" ? value.ToString() : "0");
                }
                statisticData.Lines.Add(dataItem);
            }

            List<int> summaryResults = new List<int>();

            foreach (StatisticDataItem dataItem in statisticData.Lines.Where(x => x.IsCountable))
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

            if (summaryResults.Any())
            {
                StatisticDataItem resultDataItem = new StatisticDataItem
                {
                    Id = "0-0",
                    StrongLevel = 1,
                    Title = "Всего человек: ",
                    Values = summaryResults.Select(x => x.ToString()).ToList()
                };
                statisticData.Lines.Insert(0, resultDataItem);
            }

            return new[] { statisticData };
        }

        private Dictionary<int, string> GetFilters()
        {
            var filters = Filters.GetConsolidateCriteries().Select(x => new { x.Key, x.KeyValue }).ToDictionary(x => x.Key.Key, x => x.Key.Value);
            foreach (var item in Filters.GetConsolidateCriteries().Where(x => x.Inner != null).SelectMany(x => x.Inner).ToDictionary(x => x.Key, x => x.Value))
            {
                filters.Add(item.Key, item.Value);
            }
            return filters;
        }

        private KeyValuePair<int, string> GetCurrentFilter(int id)
        {
            return GetFilters().Single(x => x.Key == id);
        }
    }
}
