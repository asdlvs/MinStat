
using System;
using System.Collections.Generic;
using System.ServiceModel;
using MinStat.DAL.POCO;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.BLL
{
    [ServiceContract(Name = "StatisticDataService", Namespace = "http://www.fem-sut.spb.ru/")]
    public interface IStatisticDataService
    {
        [OperationContract]
        IEnumerable<StatisticData> GetFullReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate);

        [OperationContract]
        IEnumerable<StatisticData> GetStaticConsolidatedReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> activities, List<int> criteries);

        [OperationContract]
        IEnumerable<StatisticData> GetDynamicConsolidatedReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> activities, List<int> criteries);

        [OperationContract]
        IEnumerable<StatisticData> GetQtyStaticReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate,
            List<int> verticalChecks, List<KeyValuePair<int, int>> horizontalChecks);

        [OperationContract]
        IEnumerable<StatisticData> GetQtyDynamicReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate,
            List<int> verticalChecks, List<KeyValuePair<int, int>> horizontalChecks);

        [OperationContract]
        IEnumerable<StatisticData> GetSummaryReport(int enterpiseId, int federalSubjectId, int federalDistrictId, DateTime boundDate, List<int> activities,
                                                        List<int> genders, List<int> educationLevels, List<int> postLevels);

        [OperationContract]
        IEnumerable<StatisticData> GetFastSummaryReport(int enterpriseId, int federalSubjectId, int federalDistrictId, int activityId);

        [OperationContract]
        List<FederalDistrict> GetFederalDistricts();

        [OperationContract]
        List<FederalSubject> GetFederalSubjects(int districtId);

        [OperationContract]
        List<Enterprise> GetEnterprises(int subjectId);

        [OperationContract]
        IEnumerable<Activity> GetActivities();

        [OperationContract]
        IEnumerable<FilterCritery> GetConsolidateFilterCritery();

        [OperationContract]
        IDictionary<int, string> GetPostLevels();

        [OperationContract]
        IDictionary<int, string> GetEducationLevels();
    }
}
