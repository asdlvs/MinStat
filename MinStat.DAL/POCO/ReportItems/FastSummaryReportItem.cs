// -----------------------------------------------------------------------
// <copyright file="FastSummaryReportItem.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MinStat.DAL.POCO.ReportItems
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public class FastSummaryReportItem
  {
    public bool Gender { get; set; }

    public int EducationLevelId { get; set; }

    public string EducationLevelTitle { get; set; }

    public int PostLevelId { get; set; }

    public string PostLevelTitle { get; set; }

    public int Count { get; set; }
    public int MiddleAge { get; set; }
    public decimal MiddleSalary { get; set; }
  }
}
