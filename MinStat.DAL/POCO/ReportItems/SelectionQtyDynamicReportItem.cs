using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinStat.DAL.POCO.ReportItems
{
    public class SelectionQtyDynamicReportItem : FullReportItem
    {
        public DateTime StartPeriodDate { get; set; }
        public DateTime EndPeriodDate { get; set; }
    }
}
