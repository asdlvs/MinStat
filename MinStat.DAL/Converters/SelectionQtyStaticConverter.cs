using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MinStat.DAL.POCO.ReportItems;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL.Converters
{
    public class SelectionQtyStaticConverter : IStatisticDataConverter<SelectionQtyStaticReportItem>
    {
        public IEnumerable<POCO.ResultItems.StatisticData> Convert(IEnumerable<SelectionQtyStaticReportItem> result)
        {
            StatisticData statisticData = new StatisticData();
            var resultList = result.ToList();
            statisticData.Titles = resultList.Select(x => new { x.PostLevelId, x.EducationLevelId, x.PostLevelTitle, x.EducationLevelTitle })
                .Distinct()
                .ToDictionary(
                    x => String.Format("{0}-{1}", x.PostLevelId, x.EducationLevelId),
                    x => String.Format("{0}_{1}", x.PostLevelTitle, x.EducationLevelTitle));

            statisticData.Lines = new List<StatisticDataItem>();

            var resultGroupedByActivity =
                resultList.GroupBy(x => new { x.ActivityId, x.ActivityTitle, x.Part1, x.Part2, x.Part3, x.Part4, x.Part5 });

            foreach (var groupedReportItem in resultGroupedByActivity)
            {
                if(groupedReportItem.Key.Part5 == 0)
                    continue;
                var item = new StatisticDataItem
                {
                    Title = String.Format("{0}.{1}{2}.{3}{4} {5}",
                                          groupedReportItem.Key.Part1,
                                          groupedReportItem.Key.Part2,
                                          groupedReportItem.Key.Part3,
                                          groupedReportItem.Key.Part4,
                                          groupedReportItem.Key.Part5,
                                          groupedReportItem.Key.ActivityTitle
                        ),
                    Id = groupedReportItem.Key.ActivityId.ToString(CultureInfo.InvariantCulture),
                    Values = new List<string>(),
                    StrongLevel = 0
                };

                foreach (var title in statisticData.Titles)
                {
                    var pointElement =
                           groupedReportItem.SingleOrDefault(x => String.Format("{0}-{1}", x.PostLevelId, x.EducationLevelId).Equals(title.Key, StringComparison.OrdinalIgnoreCase));
                    item.Values.Add(
                        pointElement == null
                                        ? "0"
                                        : pointElement.PeoplesCount.ToString());
                }
                
                 statisticData.Lines.Add(item);
            }
            statisticData.Titles.Add("", "");
            statisticData.Titles = statisticData.Titles.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

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


        public IEnumerable<StatisticData> Convert(IEnumerable<SelectionQtyStaticReportItem> result, List<int> criteries)
        {
          throw new NotImplementedException();
        }
    }
}
