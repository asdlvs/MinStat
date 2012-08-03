using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.AnalizeUI.Models
{
    public class FastSummarySelectModel
    {
        public IEnumerable<ActivityModel> Activities { get; set; }
        public IDictionary<int, string> Enterprises { get; set; }
        public IDictionary<int, string> FederalSubjects { get; set; }
        public IDictionary<int, string> FederalDistricts { get; set; }
    }
}