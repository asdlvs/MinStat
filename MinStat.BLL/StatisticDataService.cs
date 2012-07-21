using System;
using System.Collections.Generic;
using MinStat.DAL;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.BLL
{
    public class StatisticDataService : IStatisticDataService
    {
        private readonly IStatisticDataRepository _statisticDataRepository;

        public StatisticDataService(IStatisticDataRepository statisticDataRepository)
        {
            _statisticDataRepository = statisticDataRepository;
        }

        public StatisticDataService():
            this(new StatisticDataRepository())
        {
        }

        public IEnumerable<StatisticData> GetFullReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate)
        {
            return _statisticDataRepository.GetFullReportData(enterpriseId, federalDistrictId, federalDistrictId, startDate, endDate);
        }


        public IEnumerable<StatisticData> GetConsolidateReport(int enterpriseId, int federalSubjectId, int federalDistrictId, int subsectorId, DateTime startDate, DateTime endDate)
        {
            return _statisticDataRepository.GetConsolidatedReportData(enterpriseId, federalSubjectId, federalDistrictId,
                                                                      subsectorId, startDate, endDate);
        }
    }
}
