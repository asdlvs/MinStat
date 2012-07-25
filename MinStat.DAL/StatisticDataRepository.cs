using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MinStat.DAL.Converters;
using MinStat.DAL.POCO;
using MinStat.DAL.POCO.ReportItems;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL
{
    public class StatisticDataRepository : IStatisticDataRepository
    {
        readonly IStatisticDataConvertersFactory _converterFactory;
        private readonly DatabaseContext _context;

        private const string ConsolidateDataStoredProcedureCaller = "Exec dbo.GetConsolidateDate @EnterpriseId, @FederalSubjectId, @FederalDistrictId, @SubSectorId, @StartDate, @EndDate";
        private const string FullDataStoredProcedureCaller = "Exec dbo.GetFullData @EnterpriseId, @FederalSubjectId, @FederalDistrictId, @StartDate, @EndDate";
        private const string SelectionQtyStaticDataStoredProcedureCaller = "Exec dbo.GetFilterQtyStaticData @EnterpriseId, @FederalSubjectId, @FederalDistrictId, @StartDate, @EndDate, @activities, @educationPostLevels";

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

        public IEnumerable<StatisticData> GetQtyStaticReportData(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> verticalItems, List<KeyValuePair<int, int>> horizontalItems)
        {
            DataTable verticalItemsTable = new DataTable("OneIntColumnType");
            verticalItemsTable.Columns.Add("Id", typeof(int));
            foreach (int item in verticalItems)
            {
                DataRow row = verticalItemsTable.NewRow();
                row[0] = item;
                verticalItemsTable.Rows.Add(row);
            }

            DataTable horizontalItemsTable = new DataTable("TwoIntColumnType");
            horizontalItemsTable.Columns.Add("FirstId", typeof(int));
            horizontalItemsTable.Columns.Add("SecondId", typeof(int));
            foreach (var item in horizontalItems)
            {
                DataRow row = horizontalItemsTable.NewRow();
                row[0] = item.Key;
                row[1] = item.Value;
                horizontalItemsTable.Rows.Add(row);
            }

            SqlParameter activitiesParameter = new SqlParameter("@activities", SqlDbType.Structured);
            activitiesParameter.Value = verticalItemsTable;
            activitiesParameter.TypeName = "dbo.OneIntColumnType";
            SqlParameter educationPostLevelsParameter = new SqlParameter("@educationPostLevels", SqlDbType.Structured);
            educationPostLevelsParameter.Value = horizontalItemsTable;
            educationPostLevelsParameter.TypeName = "dbo.TwoIntColumnType";

            SqlParameter[] parameters = new[] { 
                new SqlParameter("@EnterpriseId", enterpriseId),
                new SqlParameter("@FederalSubjectId", federalSubjectId),
                new SqlParameter("@FederalDistrictId", federalDistrictId),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate),
                activitiesParameter,
                educationPostLevelsParameter
            };
            IEnumerable<SelectionQtyStaticReportItem> fullReportItems = _context.Database.SqlQuery<SelectionQtyStaticReportItem>(SelectionQtyStaticDataStoredProcedureCaller, parameters);
            IStatisticDataConverter<SelectionQtyStaticReportItem> converter = _converterFactory.GetConverter<SelectionQtyStaticReportItem>();
            fullReportItems = fullReportItems.ToList();
            return converter.Convert(fullReportItems);
        }

        public IEnumerable<StatisticData> GetPeronaliesReportData()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StatisticData> GetQtyDynamicReportData(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> verticalItems, List<KeyValuePair<int, int>> horizontalItems)
        {
            List<SelectionQtyDynamicReportItem> reportItems = new List<SelectionQtyDynamicReportItem>();
            for (DateTime date = startDate; date <= endDate; )
            {
                DataTable verticalItemsTable = new DataTable("OneIntColumnType");
                verticalItemsTable.Columns.Add("Id", typeof(int));
                foreach (int item in verticalItems)
                {
                    DataRow row = verticalItemsTable.NewRow();
                    row[0] = item;
                    verticalItemsTable.Rows.Add(row);
                }

                DataTable horizontalItemsTable = new DataTable("TwoIntColumnType");
                horizontalItemsTable.Columns.Add("FirstId", typeof(int));
                horizontalItemsTable.Columns.Add("SecondId", typeof(int));
                foreach (var item in horizontalItems)
                {
                    DataRow row = horizontalItemsTable.NewRow();
                    row[0] = item.Key;
                    row[1] = item.Value;
                    horizontalItemsTable.Rows.Add(row);
                }

                SqlParameter activitiesParameter = new SqlParameter("@activities", SqlDbType.Structured);
                activitiesParameter.Value = verticalItemsTable;
                activitiesParameter.TypeName = "dbo.OneIntColumnType";
                SqlParameter educationPostLevelsParameter = new SqlParameter("@educationPostLevels", SqlDbType.Structured);
                educationPostLevelsParameter.Value = horizontalItemsTable;
                educationPostLevelsParameter.TypeName = "dbo.TwoIntColumnType";

                SqlParameter[] parameters = new[] { 
                new SqlParameter("@EnterpriseId", enterpriseId),
                new SqlParameter("@FederalSubjectId", federalSubjectId),
                new SqlParameter("@FederalDistrictId", federalDistrictId),
                new SqlParameter("@StartDate", date),
                new SqlParameter("@EndDate", AddQrtl(date)),
                activitiesParameter,
                educationPostLevelsParameter
            };
                List<SelectionQtyDynamicReportItem> currentReportItems =
                    _context.Database.SqlQuery<SelectionQtyDynamicReportItem>(
                        SelectionQtyStaticDataStoredProcedureCaller, parameters).ToList();
                currentReportItems.ForEach(x =>
                {
                    x.StartPeriodDate = date;
                    x.EndPeriodDate = AddQrtl(date);
                });

                reportItems.AddRange(currentReportItems);

                date = AddQrtl(date);
            }
            IStatisticDataConverter<SelectionQtyDynamicReportItem> converter = _converterFactory.GetConverter<SelectionQtyDynamicReportItem>();
            return converter.Convert(reportItems);
        }

        private DateTime AddQrtl(DateTime date)
        {
            DateTime firstQ = new DateTime(date.Year, 3, 31);
            DateTime secondQ = new DateTime(date.Year, 6, 30);
            DateTime thirdQ = new DateTime(date.Year, 9, 30);
            DateTime forthQ = new DateTime(date.Year, 12, 31);
            DateTime returnDate = new DateTime();

            if (date < firstQ)
                returnDate = date.AddDays((firstQ - date).TotalDays);
            else if (date < secondQ)
                returnDate = date.AddDays((secondQ - date).TotalDays);
            else if (date < thirdQ)
                returnDate = date.AddDays((thirdQ - date).TotalDays);
            else if (date < forthQ)
                returnDate = date.AddDays((forthQ - date).TotalDays);
            return returnDate;

        }

        public IDictionary<int, string> GetFederalDistricts()
        {
            return _context.FederalDistricts.ToDictionary(x => x.Id, x => x.Title);
        }

        public IDictionary<int, string> GetFederalSubjects(int districtId)
        {
            return _context.FederalSubjects.Where(x => x.FederalDistrictId == districtId).ToDictionary(x => x.Id, x => x.Title);
        }

        public IDictionary<int, string> GetEnterprises(int subjectId)
        {
            return _context.Enterprises.Where(x => x.FederalSubjectId == subjectId).ToDictionary(x => x.Id, x => x.Title);
        }

        public IEnumerable<POCO.Activity> GetActivities()
        {
            return _context.Activities;
        }

        public IEnumerable<FilterCritery> GetConsolidateFilterCriteries()
        {
            List<FilterCritery> result = new List<FilterCritery>();
            FilterCritery commonCritery = new FilterCritery
                                        {
                                            Key = new KeyValuePair<int, string>(1, "Число работников, всего, чел."),
                                            Inner = new Dictionary<int, string>
                                                        {
                                                            {2, "из них мужчин"},
                                                            {3, "из них женщин"},
                                                            {4, "Принято на работу, чел."},
                                                            {5, "Уволено с работы, чел."},
                                                            {6, "Повысили квалификацию, чел."},
                                                            {7, "Прошли аттестацию, чел."}
                                                        }
                                        };
            result.Add(commonCritery);

            FilterCritery educationCritery = new FilterCritery
                                                 {
                                                     Key = new KeyValuePair<int, string>(8, "Образование:"),
                                                     Inner = new Dictionary<int, string>
                                                                 {
                                                                     {9, "ВПО, чел."},
                                                                     {10, "СПО, чел."},
                                                                     {11, "СО, чел."}
                                                                 }
                                                 };
            result.Add(educationCritery);

            FilterCritery categoryCritery = new FilterCritery
            {
                Key = new KeyValuePair<int, string>(12, "Категория:"),
                Inner = new Dictionary<int, string>
                                                     {
                                                         {13, "АУ, чел."},
                                                         {14, "ИТРиС, чел."},
                                                         {15, "ОР, чел."},
                                                         {16, "ВП, чел."},
                                                     }
            };

            FilterCritery middleAge = new FilterCritery
                                          {
                                              Key = new KeyValuePair<int, string>(17, "Средний возраст")
                                          };
            result.Add(middleAge);

            FilterCritery middleSalary = new FilterCritery
                                             {
                                                 Key = new KeyValuePair<int,string>(18,"Среднегодовой доход")
                                             };
            result.Add(middleSalary);

            return result;
        }
    }
}

