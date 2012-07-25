
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
        IEnumerable<StatisticData> GetConsolidateReport(int enterpriseId, int federalSubjectId, int federalDistrictId, int subsectorId,
                                                        DateTime startDate, DateTime endDate);

        [OperationContract]
        IEnumerable<StatisticData> GetQtyStaticReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, 
            List<int> verticalChecks,
            List<KeyValuePair<int, int>> horizontalChecks);

        [OperationContract]
        IEnumerable<StatisticData> GetQtyDynamicReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate,
            List<int> verticalChecks,
            List<KeyValuePair<int, int>> horizontalChecks);

        [OperationContract]
        IDictionary<int, string> GetFederalDistricts();

        [OperationContract]
        IDictionary<int, string> GetFederalSubjects(int districtId);

        [OperationContract]
        IDictionary<int, string> GetEnterprises(int subjectId);

        [OperationContract]
        IEnumerable<Activity> GetActivities();

        [OperationContract]
        IEnumerable<FilterCritery> GetConsolidateFilterCritery();



    }
}
