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
            return _statisticDataRepository.GetFullReportData(enterpriseId, federalSubjectId, federalDistrictId, startDate, endDate);
        }


        public IEnumerable<StatisticData> GetConsolidateReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> activities, List<int> criteries)
        {
            return _statisticDataRepository.GetConsolidatedReportData(enterpriseId, federalSubjectId, federalDistrictId,startDate, endDate, activities,criteries);
        }

        public IEnumerable<StatisticData> GetQtyStaticReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> verticalChecks, List<KeyValuePair<int, int>> horizontalChecks)
        {
            return _statisticDataRepository.GetQtyStaticReportData(enterpriseId, federalSubjectId, federalDistrictId, startDate, endDate, verticalChecks, horizontalChecks);

        }

        public IEnumerable<StatisticData> GetQtyDynamicReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> verticalChecks, List<KeyValuePair<int, int>> horizontalChecks)
        {
            return _statisticDataRepository.GetQtyDynamicReportData(enterpriseId, federalSubjectId, federalDistrictId, startDate, endDate, verticalChecks, horizontalChecks);
        }


        public IDictionary<int, string> GetFederalDistricts()
        {
            return _statisticDataRepository.GetFederalDistricts();
        }

        public IDictionary<int, string> GetFederalSubjects(int districtId)
        {
            return _statisticDataRepository.GetFederalSubjects(districtId);
        }

        public IDictionary<int, string> GetEnterprises(int subjectId)
        {
            return _statisticDataRepository.GetEnterprises(subjectId);
        }


        public IEnumerable<DAL.POCO.Activity> GetActivities()
        {
            return _statisticDataRepository.GetActivities();
        }


        public IEnumerable<DAL.POCO.FilterCritery> GetConsolidateFilterCritery()
        {
            return _statisticDataRepository.GetConsolidateFilterCriteries();
        }
    }
}
