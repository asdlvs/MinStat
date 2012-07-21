
using System;
using System.Collections.Generic;
using System.ServiceModel;
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
    }
}
