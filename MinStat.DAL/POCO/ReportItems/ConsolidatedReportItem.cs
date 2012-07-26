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
        public decimal Hire { get; set; }
        public decimal Dismiss { get; set; }
        public decimal QualificationIncrease { get; set; }
        public decimal Validate { get; set; }
        public decimal All { get; set; }
    }

}
