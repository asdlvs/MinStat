
namespace MinStat.DAL.POCO.ReportItems
{
    public class SummaryReportItem
    {
        public int ActivityId { get; set; }
        public string ActivityTitle { get; set; }
        public int PostLevelId { get; set; }
        public string PostLevelTitle { get; set; }
        public int EducationLevelId { get; set; }
        public string EducationLevelTitle { get; set; }
        public bool Gender { get; set; }
        public int Count { get; set; }
        public int MiddleAge { get; set; }
        public decimal MiddleSalary { get; set; }
    }
}
