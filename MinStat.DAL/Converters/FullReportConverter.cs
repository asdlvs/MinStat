using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MinStat.DAL.POCO.ReportItems;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL.Converters
{
    public class FullReportConverter : IStatisticDataConverter<FullReportItem>
    {
        public IEnumerable<StatisticData> Convert(IEnumerable<FullReportItem> result)
        {
            var statisticData = new StatisticData
                                    {
                                        Titles = new Dictionary<string, string>()
                                                     {
                                                         {"","Код ОКВЭД / Название"},
                                                         {"1-1","АУ_СО"},
                                                         {"1-2","АУ_СПО"},
                                                         {"1-3","АУ_ВО"},
                                                         {"2-1","ИТРиС_СО"},
                                                         {"2-2","ИТРиС_СПО"},
                                                         {"2-3","ИТРиС_ВО"},
                                                         {"3-1","ОР_СО"},
                                                         {"3-2","ОР_СПО"},
                                                         {"3-3","ОР_ВО"},
                                                         {"4-1","ВП_СО"},
                                                         {"4-2","ВП_СПО"},
                                                         {"4-3","ВП_ВО"}
                                                     },
                                        Lines = new List<StatisticDataItem>()
                                    };


            var resultGroupedByActivity =
                result.GroupBy(x => new { x.ActivityId, x.ActivityTitle, x.Part1, x.Part2, x.Part3, x.Part4, x.Part5 });
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
                                   StrongLevel = groupedReportItem.Key.Part4 == 0 && groupedReportItem.Key.Part5 == 0 ? 2 : groupedReportItem.Key.Part5 == 0 ? 1 : 0
                               };
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 3; j++)
                    {
                        var pointElement =
                            groupedReportItem.SingleOrDefault(x => x.PostLevelId == i && x.EducationLevelId == j);
                        item.Values.Add(
                            pointElement == null
                                            ? "0"
                                            : pointElement.PeoplesCount.ToString(CultureInfo.InvariantCulture));
                    }
                }
                statisticData.Lines.Add(item);
            }

            return new List<StatisticData> { statisticData };
        }


        public IEnumerable<StatisticData> Convert(IEnumerable<FullReportItem> result, List<int> criteries)
        {
            throw new NotImplementedException();
        }
    }
}
