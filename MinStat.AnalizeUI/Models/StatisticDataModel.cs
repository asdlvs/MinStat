using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.AnalizeUI.Models
{
    public class StatisticDataModel
    {
        public IDictionary<string, string> Titles { get; set; }
        public IEnumerable<StatisticDataItemModel> Values { get; set; }
        public string ReportName { get; set; }
        public string CreatedDateTime { get; set; }
        public string Activity { get; set; }
        public string MainActivity { get; set; }
        public string FederalDistrict { get; set; }
        public string FederalSubject { get; set; }
        public string Enterprise { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Categories { get; set; }
    }
}