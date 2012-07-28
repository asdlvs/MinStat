using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinStat.DAL.POCO.ReportItems
{
    public class ConsolidatedDynamicReportItem : ConsolidatedStaticReportItem
    {
        public DateTime StartPeriodDate { get; set; }
        public DateTime EndPeriodDate { get; set; }
    }
}
