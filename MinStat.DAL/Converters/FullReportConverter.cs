using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MinStat.DAL.POCO.ReportItems;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL.Converters
{
    class FullReportConverter : IStatisticDataConverter<FullReportItem>
    {
        public IEnumerable<StatisticData> Convert(IEnumerable<FullReportItem> result)
        {
            var statisticData = new StatisticData
                                    {
                                        Titles = new List<string>
                                                     {
                                                         "Код ОКВЭД / Название",
                                                         "АУ_СО",
                                                         "АУ_СПО",
                                                         "АУ_ВО",
                                                         "ИТРиС_СО",
                                                         "ИТРиС_СПО",
                                                         "ИТРиС_ВО",
                                                         "ОР_СО",
                                                         "ОР_СПО",
                                                         "ОР_ВО",
                                                         "ВП_СО",
                                                         "ВП_СПО",
                                                         "ВП_ВО"
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
                                   Values = new List<string>(),
                                   StrongLevel = groupedReportItem.Key.Part4 == 0 && groupedReportItem.Key.Part5 == 0 ? 2 : groupedReportItem.Key.Part5 == 0 ? 1 : 0
                               };
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 3; j++)
                    {
                        var pointElement =
                            groupedReportItem.SingleOrDefault(x => x.PostLevelId == i && x.EducationLevelId == j);
                        item.Values.Add(pointElement == null
                                            ? "0"
                                            : pointElement.PeoplesCount.ToString(CultureInfo.InvariantCulture));
                    }
                }
                statisticData.Lines.Add(item);
            }

            return new List<StatisticData> { statisticData };
        }
    }
}
