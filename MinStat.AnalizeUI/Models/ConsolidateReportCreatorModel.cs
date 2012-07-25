using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.AnalizeUI.Models
{
    public class ConsolidateReportCreatorModel
    {
        public IEnumerable<ActivityModel> Activities { get; set; }

        public IEnumerable<FilterCriteryModel> Criteries { get; set; }

        public int EnterpriseId { get; set; }

        public int FederalSubjectId { get; set; }

        public int FederalDistrictId { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<int> SelectedActivities { get; set; }

        public List<int> SelectedCriteries { get; set; }
    }

}