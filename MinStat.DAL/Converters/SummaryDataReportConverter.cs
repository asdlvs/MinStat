using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MinStat.DAL.POCO.ReportItems;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL.Converters
{
    public class SummaryDataReportConverter : IStatisticDataConverter<SummaryReportItem>
    {
        public IEnumerable<StatisticData> Convert(IEnumerable<SummaryReportItem> result)
        {
            StatisticData statisticData = new StatisticData();
            var resultList = result.ToList();
            statisticData.Titles = new Dictionary<string, string> { 
            { "0", "" }, 
            { "1", "Количество" }, 
            { "2", "%"},
            { "3", "Средний возраст" }, 
            { "4", "Средний годовой доход (руб. )" } };
            statisticData.Lines = new List<StatisticDataItem>();

            if (resultList.Any())
            {
                var groupedByActivityResult = resultList.GroupBy(x => new { x.ActivityId, x.ActivityTitle });
                foreach (var groupedItem in groupedByActivityResult)
                {
                    StatisticDataItem dataItem = new StatisticDataItem();
                    dataItem.StrongLevel = 1;
                    dataItem.Title = groupedItem.Key.ActivityTitle;
                    dataItem.Values = new List<string>
                  {
                    groupedItem.Sum(x => x.Count).ToString(CultureInfo.InvariantCulture),
                    GetPersentage(groupedItem.Sum(x => x.Count), resultList.Sum(x => x.Count)),
                    groupedItem.Average(x => x.MiddleAge).ToString("0.0"),
                    groupedItem.Average(x => x.MiddleSalary).ToString("0")
                  };
                    statisticData.Lines.Add(dataItem);
                    var groupedByGPEItem =
                      groupedItem.GroupBy(
                        x =>
                        String.Format(
                          "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Пол: {0}, Категория: {1}, Образование: {2}",
                          x.Gender ? "М" : "Ж",
                          x.PostLevelTitle,
                          x.EducationLevelTitle));

                    foreach (var gpeGroupedItem in groupedByGPEItem)
                    {
                        StatisticDataItem innerDataItem = new StatisticDataItem();
                        innerDataItem.Title = gpeGroupedItem.Key;
                        innerDataItem.Values = new List<string>
                    {
                      gpeGroupedItem.Sum(x => x.Count).ToString(CultureInfo.InvariantCulture),
                      "",
                      gpeGroupedItem.Average(x => x.MiddleAge).ToString("0.0"),
                      gpeGroupedItem.Average(x => x.MiddleSalary).ToString("0")
                    };
                        statisticData.Lines.Add(innerDataItem);
                    }
                }
                StatisticDataItem globalDataItem = new StatisticDataItem();
                globalDataItem.Title = "Итого: ";
                globalDataItem.StrongLevel = 2;
                globalDataItem.Values = new List<string>
                {
                  resultList.Sum(x => x.Count).ToString(CultureInfo.InvariantCulture),
                  "100%",
                  resultList.Average(x => x.MiddleAge).ToString("0.0"),
                  resultList.Average(x => x.MiddleSalary).ToString("0")
                };

                statisticData.Lines.Add(globalDataItem);
            }
            return new List<StatisticData> { statisticData };
        }

        //ToDo: remove duplicate
        private string GetPersentage(decimal a, decimal b)
        {
            return string.Format("{0:N1}%", ((a / b) * 100));
        }

        public IEnumerable<POCO.ResultItems.StatisticData> Convert(IEnumerable<SummaryReportItem> result, List<int> criteries)
        {
            throw new NotImplementedException();
        }
    }
}
