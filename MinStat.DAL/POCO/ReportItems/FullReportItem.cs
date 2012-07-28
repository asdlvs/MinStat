
namespace MinStat.DAL.POCO.ReportItems
{
    public class FullReportItem
    {
        public int ActivityId { get; set; }
        public int Part1 { get; set; }
        public int Part2 { get; set; }
        public int Part3 { get; set; }
        public int Part4 { get; set; }
        public int Part5 { get; set; }
        public string ActivityTitle { get; set; }
        public int? EducationLevelId { get; set; }
        public string EducationLevelTitle { get; set; }
        public int? PostLevelId { get; set; }
        public string PostLevelTitle { get; set; }
        public decimal PeoplesCount { get; set; }
    }
}
