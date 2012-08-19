using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.AnalizeUI.Models
{
    public class FastSummarySelectModel
    {
        public IEnumerable<ActivityModel> Activities { get; set; }
        public IEnumerable<EnterpriseModel> Enterprises { get; set; }
        public IEnumerable<FederalSubjectModel> FederalSubjects { get; set; }
        public IEnumerable<FederalDistrictModel> FederalDistricts { get; set; }
    }
}