using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinStat.DAL.POCO.ReportItems;
using MinStat.DAL.POCO;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL
{
    public interface IStatisticDataRepository
    {
        IEnumerable<StatisticData> GetStaticConsolidatedReportData(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> activities, List<int> criteries);
        IEnumerable<StatisticData> GetDynamicConsolidatedReportData(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> activities, List<int> criteries);

        IEnumerable<StatisticData> GetFullReportData(int enterpriseId,int federalSubjectId,int federalDistrictId,DateTime startDate,DateTime endDate);

        IEnumerable<StatisticData> GetPeronaliesReportData();

        IEnumerable<StatisticData> GetQtyStaticReportData(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> verticalItems, 
            List<KeyValuePair<int, int>> horizontalItems);

        IEnumerable<StatisticData> GetQtyDynamicReportData(int enterpriseId,int federalSubjectId,int federalDistrictId,DateTime startDate,DateTime endDate,List<int> verticalItems,
            List<KeyValuePair<int, int>> horizontalItems);

        IEnumerable<StatisticData> GetSummaryReportData(int enterpiseId, int federalSubjectId, int federalDistrictId,DateTime boundDate, List<int> activities,
                                                        List<int> genders, List<int> educationLevels,List<int> postLevels);

        IEnumerable<StatisticData> GetFastSummaryReportData(int enterpriseId, int federalSubjectId, int federalDistrictId, int activityId);

        List<FederalDistrict> GetFederalDistricts();
        List<FederalSubject> GetFederalSubjects(int districtId);
        List<Enterprise> GetEnterprises(int sibjectId);

        IEnumerable<Activity> GetActivities();

        IEnumerable<FilterCritery> GetConsolidateFilterCriteries();

        IEnumerable<PostLevel> PostLevels();

        IEnumerable<EducationLevel> EducationLevels();
    }
}
