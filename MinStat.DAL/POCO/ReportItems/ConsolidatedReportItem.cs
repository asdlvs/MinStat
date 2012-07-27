using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinStat.DAL.POCO.ReportItems
{
  public class ConsolidatedReportItem
  {
    public bool Gender { get; set; }
    public int EducationLevelId { get; set; }
    public int PostLevelId { get; set; }
    public int ActivityId { get; set; }

    public string ActivityTitle { get; set; }

    public int Part_1 { get; set; }
    public int Part_2 { get; set; }
    public int Part_3 { get; set; }
    public int Part_4 { get; set; }
    public int Part_5 { get; set; }
    public decimal Hire { get; set; }
    public decimal Dismiss { get; set; }
    public decimal QualificationIncrease { get; set; }
    public decimal Validate { get; set; }
    public decimal All { get; set; }
  }

}
