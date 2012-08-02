using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.AnalizeUI.Models
{
    public class SummaryReportCreatorModel
    {
        public IEnumerable<ActivityModel> Activities { get; set; }

        public IDictionary<int, string> EducationLevels { get; set; }

        public IDictionary<int, string> PostLevels { get; set; }

        public int EnterpriseId { get; set; }

        public int FederalDistrictId { get; set; }

        public int FederalSubjectId { get; set; }

        public IEnumerable<int> SelectedActivities { get; set; }
        public IEnumerable<int> SelectedEducationLevels { get; set; }

        public IEnumerable<int> SelectedPostLevels { get; set; }

        public IEnumerable<int> SelectedGenders { get; set; }

        public string BoundDate { get; set; }
    }
}