using System.Globalization;
using MinStat.DAL.HardCode;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MinStat.DAL.POCO.ReportItems;

    //TODO: дублирование кода

    public class FastSummaryReportConverter : IStatisticDataConverter<FastSummaryReportItem>
    {
        public IEnumerable<POCO.ResultItems.StatisticData> Convert(IEnumerable<FastSummaryReportItem> result)
        {

            var resultList = result.ToList();
            StatisticData statisticData = new StatisticData();
            statisticData.Titles = new Dictionary<string, string>
                                       {
                                           {"1", ""},
                                           {"2", ""},
                                           {"3", ""}
                                       };
            statisticData.Lines = new List<StatisticDataItem>();
            int commonCount = resultList.Sum(x => x.Count);
            if (commonCount > 0)
            {
                int menCount = resultList.Where(x => x.Gender).Sum(x => x.Count);
                int womenCount = resultList.Where(x => !x.Gender).Sum(x => x.Count);

                statisticData.Lines.Add(GetAggregateValue(1, commonCount, commonCount, strongLevel: 1));
                statisticData.Lines.Add(GetAggregateValue(2, menCount, commonCount));
                statisticData.Lines.Add(GetAggregateValue(3, womenCount, commonCount));

                int soEducationLevelCount = resultList.Where(x => x.EducationLevelId == 1).Sum(x => x.Count);
                int spoEducationLevelCount = resultList.Where(x => x.EducationLevelId == 2).Sum(x => x.Count);
                int vpoEducationLevelCount = resultList.Where(x => x.EducationLevelId == 3).Sum(x => x.Count);

                statisticData.Lines.Add(GetAggregateValue(8, commonCount, commonCount, strongLevel: 1));
                statisticData.Lines.Add(GetAggregateValue(9, vpoEducationLevelCount, commonCount));
                statisticData.Lines.Add(GetAggregateValue(10, spoEducationLevelCount, commonCount));
                statisticData.Lines.Add(GetAggregateValue(11, soEducationLevelCount, commonCount));

                int auPostLevelCount = resultList.Where(x => x.PostLevelId == 1).Sum(x => x.Count);
                int itrisPostLevelCount = resultList.Where(x => x.PostLevelId == 2).Sum(x => x.Count);
                int orPostLevelCount = resultList.Where(x => x.PostLevelId == 3).Sum(x => x.Count);
                int vpPostLevelCount = resultList.Where(x => x.PostLevelId == 4).Sum(x => x.Count);

                statisticData.Lines.Add(GetAggregateValue(12, commonCount, commonCount, strongLevel: 1));
                statisticData.Lines.Add(GetAggregateValue(13, auPostLevelCount, commonCount));
                statisticData.Lines.Add(GetAggregateValue(14, itrisPostLevelCount, commonCount));
                statisticData.Lines.Add(GetAggregateValue(15, orPostLevelCount, commonCount));
                statisticData.Lines.Add(GetAggregateValue(16, vpPostLevelCount, commonCount));
                statisticData.Lines.Add(GetAggregateValue(17, (decimal)resultList.Average(x => x.MiddleAge), 0, "лет", strongLevel: 1));
                statisticData.Lines.Add(GetAggregateValue(18, resultList.Average(x => x.MiddleSalary), 0, "руб", strongLevel: 1));
            }
            return new[] { statisticData };
        }



        private StatisticDataItem GetAggregateValue(int criteryId, decimal value, decimal commonCount = 0, string subscribe = "", int strongLevel = 0)
        {
            var filters = Filters.GetConsolidateCriteries().Select(x => new { x.Key, x.KeyValue }).ToDictionary(x => x.Key.Key, x => x.Key.Value);
            foreach (var item in Filters.GetConsolidateCriteries().Where(x => x.Inner != null).SelectMany(x => x.Inner).ToDictionary(x => x.Key, x => x.Value))
            {
                filters.Add(item.Key, item.Value);
            }

            StatisticDataItem statisticDataLine = new StatisticDataItem();
            statisticDataLine.Title = filters.Single(x => x.Key == criteryId).Value;
            statisticDataLine.Values = new List<string>();
            statisticDataLine.Values.Add(((int)value).ToString(CultureInfo.InvariantCulture));
            if (commonCount != 0)
            {
                statisticDataLine.Values.Add(GetPersentage(value, commonCount));
            }
            else
            {
                statisticDataLine.Values.Add(subscribe);
            }
            statisticDataLine.StrongLevel = strongLevel;
            return statisticDataLine;
        }

        private string GetPersentage(decimal a, decimal b)
        {
            return string.Format("{0:N1}%", ((a / b) * 100));
        }
        public IEnumerable<POCO.ResultItems.StatisticData> Convert(IEnumerable<FastSummaryReportItem> result, List<int> criteries)
        {
            throw new NotImplementedException();
        }
    }
}
