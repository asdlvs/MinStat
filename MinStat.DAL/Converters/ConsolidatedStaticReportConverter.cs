using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MinStat.DAL.POCO.ReportItems;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL.Converters
{
    using MinStat.DAL.HardCode;
    using MinStat.DAL.POCO;

    class ConsolidatedStaticReportConverter : IStatisticDataConverter<ConsolidatedStaticReportItem>
    {
        public IEnumerable<StatisticData> Convert(IEnumerable<ConsolidatedStaticReportItem> result)
        {
            List<ConsolidatedStaticReportItem> resultList = result.ToList();
            StatisticData statisticData = new StatisticData();
            statisticData.Titles = new Dictionary<string, string>
                                       {
                                           {"1", ""},
                                           {"2", ""},
                                           {"3", ""}
                                       };
            statisticData.Lines = new List<StatisticDataItem>();
            decimal commonCount = resultList.Sum(x => x.All);
            if (commonCount > 0)
            {
                statisticData.Lines.Add(GetAggregateValue(1, commonCount, commonCount));
                statisticData.Lines.Add(GetAggregateValue(2, resultList.Where(x => x.Gender == 1).Sum(x => x.All),commonCount));
                statisticData.Lines.Add(GetAggregateValue(3, resultList.Where(x => x.Gender == 0).Sum(x => x.All),commonCount));
                statisticData.Lines.Add(GetAggregateValue(4, resultList.Sum(x => x.Hire), commonCount));
                statisticData.Lines.Add(GetAggregateValue(5, resultList.Sum(x => x.Dismiss), commonCount));
                statisticData.Lines.Add(GetAggregateValue(6, resultList.Sum(x => x.QualificationIncrease), commonCount));
                statisticData.Lines.Add(GetAggregateValue(7, resultList.Sum(x => x.Validate), commonCount));
                statisticData.Lines.Add(GetAggregateValue(8, commonCount, commonCount));
                statisticData.Lines.Add(GetAggregateValue(9,resultList.Where(x => x.EducationLevelId == 3).Sum(x => x.All),commonCount));
                statisticData.Lines.Add(GetAggregateValue(10,resultList.Where(x => x.EducationLevelId == 2).Sum(x => x.All),commonCount));
                statisticData.Lines.Add(GetAggregateValue(11,resultList.Where(x => x.EducationLevelId == 1).Sum(x => x.All),commonCount));
                statisticData.Lines.Add(GetAggregateValue(12, commonCount, commonCount));
                statisticData.Lines.Add(GetAggregateValue(13, resultList.Where(x => x.PostLevelId == 1).Sum(x => x.All),commonCount));
                statisticData.Lines.Add(GetAggregateValue(14, resultList.Where(x => x.PostLevelId == 2).Sum(x => x.All),commonCount));
                statisticData.Lines.Add(GetAggregateValue(15, resultList.Where(x => x.PostLevelId == 3).Sum(x => x.All),commonCount));
                statisticData.Lines.Add(GetAggregateValue(16, resultList.Where(x => x.PostLevelId == 4).Sum(x => x.All),commonCount));

                statisticData.Lines.Add(GetAggregateValue(17, (decimal)resultList.Average(x => x.MiddleAge),0, "лет"));
                statisticData.Lines.Add(GetAggregateValue(18, resultList.Average(x => x.MiddleSalary),0, "руб"));
            }
            return new[] { statisticData };
        }

        private StatisticDataItem GetAggregateValue(int criteryId, decimal value, decimal commonCount = 0, string subscribe = "")
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
            return statisticDataLine;
        }

        private string GetPersentage(decimal a, decimal b)
        {
            return string.Format("{0:N1}%", ((a / b) * 100));
        }


        public IEnumerable<StatisticData> Convert(IEnumerable<ConsolidatedStaticReportItem> result, List<int> criteries)
        {
            throw new NotImplementedException();
        }
    }
}
