// -----------------------------------------------------------------------
// <copyright file="FastSummaryReportConverter.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MinStat.DAL.Converters
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using MinStat.DAL.POCO.ReportItems;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public class FastSummaryReportConverter : IStatisticDataConverter<FastSummaryReportItem>
  {
    public IEnumerable<POCO.ResultItems.StatisticData> Convert(IEnumerable<FastSummaryReportItem> result)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<POCO.ResultItems.StatisticData> Convert(IEnumerable<FastSummaryReportItem> result, List<int> criteries)
    {
      throw new NotImplementedException();
    }
  }
}
