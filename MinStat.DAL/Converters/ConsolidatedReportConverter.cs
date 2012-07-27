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

  class ConsolidatedReportConverter : IStatisticDataConverter<ConsolidatedReportItem>
  {
    public IEnumerable<StatisticData> Convert(IEnumerable<ConsolidatedReportItem> result)
    {
      return null;
    }

    public IEnumerable<StatisticData> Convert(IEnumerable<ConsolidatedReportItem> result, List<int> criteries)
    {
      var groupedByActivityData =
        result.GroupBy(x => new Activity
          {
            Id = x.ActivityId,
            Title = x.ActivityTitle,
            Part_1 = x.Part_1,
            Part_2 = x.Part_2,
            Part_3 = x.Part_3,
            Part_4 = x.Part_4,
            Part_5 = x.Part_5
          });
      List<StatisticData> statisticData = new List<StatisticData>();

      foreach (var gi in groupedByActivityData)
      {
        StatisticData statisticDataItem = new StatisticData();

        statisticDataItem.Titles = new Dictionary<string, string>
          {
            {"1", String.Format("{0}.{1}{2}.{3}{4} {5}", gi.Key.Part_1, gi.Key.Part_2, gi.Key.Part_3, gi.Key.Part_4, gi.Key.Part_5, gi.Key.Title)},
            {"2",String.Empty},
            {"3",String.Empty}
          };
        statisticDataItem.Lines = new List<StatisticDataItem>();

        if (criteries.Any(x => x <= 9 && x >= 2))
        {
          statisticDataItem.Lines.Add(this.GetAggregateValue(1, gi.Count()));
        }
        if (criteries.Contains(2))
        {
          statisticDataItem.Lines.Add(this.GetAggregateValue(2, gi.Count(x => x.Gender)));
        }
        if (criteries.Contains(3))
        {
          statisticDataItem.Lines.Add(this.GetAggregateValue(3, gi.Count(x => !x.Gender)));
        }
        if (criteries.Contains(4))
        {
          statisticDataItem.Lines.Add(this.GetAggregateValue(4, gi.Sum(x => x.Hire)));
        }
        if (criteries.Contains(5))
        {
          statisticDataItem.Lines.Add(this.GetAggregateValue(5, gi.Sum(x => x.Dismiss)));
        }
        if (criteries.Contains(6))
        {
          statisticDataItem.Lines.Add(this.GetAggregateValue(6, gi.Sum(x => x.QualificationIncrease)));
        }
        if (criteries.Contains(7))
        {
          statisticDataItem.Lines.Add(this.GetAggregateValue(7, gi.Sum(x => x.Validate)));
        }

        statisticData.Add(statisticDataItem);
      }

      return statisticData;
    }

    private StatisticDataItem GetAggregateValue(int criteryId, decimal value)
    {
      var filters = Filters.GetConsolidateCriteries().Select(x => new{ x.Key, x.KeyValue}).ToDictionary(x => x.Key.Key, x => x.Key.Value);
      foreach (var item in Filters.GetConsolidateCriteries().Where(x => x.Inner != null).SelectMany(x => x.Inner).ToDictionary(x => x.Key, x => x.Value))
      {
        filters.Add(item.Key, item.Value);
      }
      
      StatisticDataItem statisticDataLine = new StatisticDataItem();
      statisticDataLine.Title = filters.Single(x => x.Key == criteryId).Value;
      statisticDataLine.Values = new List<string>();
      statisticDataLine.Values.Add(value.ToString());
      return statisticDataLine;
    }

    private string GetPersentage(int a, int b)
    {
      return string.Format("{0}%", ((decimal)a / (decimal)b) * 100);
    }
  }
}
