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
            statisticData.Titles = result.Select(x => new {x.PostLevelId, x.EducationLevelId, x.PostLevelTitle, x.EducationLevelTitle})
                .Distinct()
                .ToDictionary(
                    x => String.Format("{0}-{1}", x.PostLevelId, x.EducationLevelId),
                    x => String.Format("{0}_{1}", x.PostLevelTitle, x.EducationLevelTitle));

            statisticData.Lines = new List<StatisticDataItem>();

            var resultGroupedByActivity =
                result.GroupBy(x => new {x.ActivityId, x.ActivityTitle, x.Part1, x.Part2, x.Part3, x.Part4, x.Part5});

            foreach (var groupedReportItem in resultGroupedByActivity)
            {
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
                                        : pointElement.PeoplesCount.ToString("#.##"));
                }
                statisticData.Lines.Add(item);
            }
            statisticData.Titles.Add("", "");
            statisticData.Titles = statisticData.Titles.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            return new List<StatisticData> { statisticData };
        }


        public IEnumerable<StatisticData> Convert(IEnumerable<SelectionQtyStaticReportItem> result, List<int> criteries)
        {
          throw new NotImplementedException();
        }
    }
}
