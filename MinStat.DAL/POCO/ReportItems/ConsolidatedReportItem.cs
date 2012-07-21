using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinStat.DAL.POCO.ReportItems
{
    public class ConsolidatedReportItem
    {
        public int SummaryId { get; set; }
        public int PersonId { get; set; }
        public string PersonTitle { get; set; }
        public string Post { get; set; }
        public int PostLevelId { get; set; }
        public int EducationLevelId { get; set; }
        public decimal YearSalary { get; set; }
        public bool Gender { get; set; }
        public bool WasQualificationIncrease { get; set; }
        public bool WasValidate { get; set; }
        public int BirthYear { get; set; }
        public bool IsHiring { get; set; }
        public int StartPostYear { get; set; }
        public bool IsDissmissed { get; set; }
    }
}
