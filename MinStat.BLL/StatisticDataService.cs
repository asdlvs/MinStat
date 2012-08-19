using System;
using System.Collections.Generic;
using System.Linq;
using MinStat.DAL;
using MinStat.DAL.POCO;
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


        public IEnumerable<StatisticData> GetStaticConsolidatedReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> activities, List<int> criteries)
        {
            return _statisticDataRepository.GetStaticConsolidatedReportData(enterpriseId, federalSubjectId, federalDistrictId, startDate, endDate, activities, criteries);
        }

        public IEnumerable<StatisticData> GetDynamicConsolidatedReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> activities, List<int> criteries)
        {
            return _statisticDataRepository.GetDynamicConsolidatedReportData(enterpriseId, federalSubjectId, federalDistrictId, startDate, endDate, activities, criteries);
        }

        public IEnumerable<StatisticData> GetQtyStaticReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> verticalChecks, List<KeyValuePair<int, int>> horizontalChecks)
        {
            return _statisticDataRepository.GetQtyStaticReportData(enterpriseId, federalSubjectId, federalDistrictId, startDate, endDate, verticalChecks, horizontalChecks);

        }

        public IEnumerable<StatisticData> GetQtyDynamicReport(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> verticalChecks, List<KeyValuePair<int, int>> horizontalChecks)
        {
            return _statisticDataRepository.GetQtyDynamicReportData(enterpriseId, federalSubjectId, federalDistrictId, startDate, endDate, verticalChecks, horizontalChecks);
        }

        public List<FederalDistrict> GetFederalDistricts()
        {
            return _statisticDataRepository.GetFederalDistricts();
        }

        public List<FederalSubject> GetFederalSubjects(int districtId)
        {
            return _statisticDataRepository.GetFederalSubjects(districtId);
        }

        public List<Enterprise> GetEnterprises(int subjectId)
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

        public IEnumerable<StatisticData> GetSummaryReport(int enterpiseId, int federalSubjectId, int federalDistrictId, DateTime boundDate, List<int> activities, List<int> genders, List<int> educationLevels, List<int> postLevels)
        {
            return _statisticDataRepository.GetSummaryReportData(enterpiseId, federalSubjectId, federalDistrictId, boundDate, activities, genders, educationLevels,postLevels);
        }


        public IDictionary<int, string> GetPostLevels()
        {
            return _statisticDataRepository.PostLevels().ToDictionary(x => x.Id, x => x.Title);
        }

        public IDictionary<int, string> GetEducationLevels()
        {
            return _statisticDataRepository.EducationLevels().ToDictionary(x => x.Id, x => x.Title);
        }


        public IEnumerable<StatisticData> GetFastSummaryReport(int enterpriseId, int federalSubjectId, int federalDistrictId, int activityId)
        {
          return _statisticDataRepository.GetFastSummaryReportData(enterpriseId, federalSubjectId, federalDistrictId, activityId);
        }
    }
}
