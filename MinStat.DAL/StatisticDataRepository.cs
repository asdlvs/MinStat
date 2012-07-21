using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MinStat.DAL.Converters;
using MinStat.DAL.POCO.ReportItems;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL
{
    public class StatisticDataRepository : IStatisticDataRepository
    {
        IStatisticDataConvertersFactory _converterFactory;
        private DatabaseContext _context;

        private const string ConsolidateDataStoredProcedureCaller = "Exec dbo.GetConsolidateDate @EnterpriseId, @FederalSubjectId, @FederalDistrictId, @SubSectorId, @StartDate, @EndDate";
        private const string FullDataStoredProcedureCaller = "Exec dbo.GetFullData @EnterpriseId, @FederalSubjectId, @FederalDistrictId, @StartDate, @EndDate";

        public StatisticDataRepository(IStatisticDataConvertersFactory converterFactory, DatabaseContext contextAdapter)
        {
            _converterFactory = converterFactory;
            _context = contextAdapter;
        }

        public StatisticDataRepository() :
            this(new StatisticDataConvertersFactory(), new DatabaseContext())
        { }

        public IEnumerable<StatisticData> GetConsolidatedReportData(int enterpriseId, int federalSubjectId, int federalDistrictId, int subsectorId, DateTime startDate, DateTime endDate)
        {
            SqlParameter[] parameters = new[]
                                            {
                                                new SqlParameter("@EnterpriseId", enterpriseId),
                                                new SqlParameter("@FederalSubjectId", federalSubjectId),
                                                new SqlParameter("@FederalDistrictId", federalDistrictId),
                                                 new SqlParameter("@SubsectorId", subsectorId),
                                                new SqlParameter("@StartDate", startDate),
                                                new SqlParameter("@EndDate", endDate)
                                            };
            IEnumerable<ConsolidatedReportItem> consolidatedReportItems =
                _context.Database.SqlQuery<ConsolidatedReportItem>(ConsolidateDataStoredProcedureCaller, parameters);
            IStatisticDataConverter<ConsolidatedReportItem> converter =
                _converterFactory.GetConverter<ConsolidatedReportItem>();
            consolidatedReportItems = consolidatedReportItems.ToList();
            return converter.Convert(consolidatedReportItems);

        }

        public IEnumerable<StatisticData> GetFullReportData(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate)
        {
            SqlParameter[] parameters = new[] { 
                new SqlParameter("@EnterpriseId", enterpriseId),
                new SqlParameter("@FederalSubjectId", federalSubjectId),
                new SqlParameter("@FederalDistrictId", federalDistrictId),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            IEnumerable<FullReportItem> fullReportItems = _context.Database.SqlQuery<FullReportItem>(FullDataStoredProcedureCaller, parameters);
            IStatisticDataConverter<FullReportItem> converter = _converterFactory.GetConverter<FullReportItem>();
            fullReportItems = fullReportItems.ToList();
            return converter.Convert(fullReportItems);
        }

        public IEnumerable<StatisticData> GetPeronaliesReportItem()
        {
            throw new NotImplementedException();
        }
    }
}
