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
    using MinStat.DAL.HardCode;

    //TODO: Говно с критериями
    public class StatisticDataRepository : IStatisticDataRepository
    {
        private readonly IStatisticDataConvertersFactory _converterFactory;
        private readonly DatabaseContext _context;

        private const string ConsolidateDataStoredProcedureCaller =
            "Exec dbo.GetConsolidateDate @EnterpriseId, @FederalSubjectId, @FederalDistrictId, @StartDate, @EndDate, @Activities";

        private const string FullDataStoredProcedureCaller =
            "Exec dbo.GetFullData @EnterpriseId, @FederalSubjectId, @FederalDistrictId, @StartDate, @EndDate";

        private const string SelectionQtyStaticDataStoredProcedureCaller =
            "Exec dbo.GetFilterQtyStaticData @EnterpriseId, @FederalSubjectId, @FederalDistrictId, @StartDate, @EndDate, @activities, @educationPostLevels";

        private const string SummaryDataStoredProcedureCaller =
            "Exec dbo.GetSummaryData @genders, @educationlevels, @postlevels, @bounddate, @enterpriseid, @federalsubjectid, @federaldistrictid, @activities";

        private const string FastSummaryStoredProcedureCaller = "Exec [dbo].[GetFastSummaryData] @enterpriseid, @federalsubjectid, @federaldistrictid, @activityid";

        public StatisticDataRepository(IStatisticDataConvertersFactory converterFactory, DatabaseContext contextAdapter)
        {
            _converterFactory = converterFactory;
            _context = contextAdapter;
        }

        public StatisticDataRepository() :
            this(new StatisticDataConvertersFactory(), new DatabaseContext())
        {
        }

        public IEnumerable<StatisticData> GetStaticConsolidatedReportData(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> activities, List<int> criteries)
        {
            DataTable dtActivities = CreateOneRowDataTable(activities, "OneIntColumnType");
            SqlParameter activitiesParameter = CreateSqlParameter(dtActivities, "@Activities", "dbo.OneIntColumnType");

            SqlParameter[] parameters = new[]
                                            {
                                                new SqlParameter("@EnterpriseId", enterpriseId),
                                                new SqlParameter("@FederalSubjectId", federalSubjectId),
                                                new SqlParameter("@FederalDistrictId", federalDistrictId),
                                                new SqlParameter("@StartDate", startDate),
                                                new SqlParameter("@EndDate", endDate),
                                                activitiesParameter
                                            };
            IEnumerable<ConsolidatedStaticReportItem> consolidatedReportItems =
                _context.Database.SqlQuery<ConsolidatedStaticReportItem>(ConsolidateDataStoredProcedureCaller,
                                                                         parameters);
            IStatisticDataConverter<ConsolidatedStaticReportItem> converter =
                _converterFactory.GetConverter<ConsolidatedStaticReportItem>();
            consolidatedReportItems = consolidatedReportItems.ToList();
            return converter.Convert(consolidatedReportItems);

        }

        public IEnumerable<StatisticData> GetDynamicConsolidatedReportData(int enterpriseId, int federalSubjectId, int federalDistrictId, DateTime startDate, DateTime endDate, List<int> activities, List<int> criteries)
        {
            List<ConsolidatedDynamicReportItem> reportItems = new List<ConsolidatedDynamicReportItem>();
            for (DateTime date = startDate; date <= endDate; )
            {
                DataTable dtActivities = CreateOneRowDataTable(activities, "OneIntColumnType");
                SqlParameter activitiesParameter = CreateSqlParameter(dtActivities, "@Activities", "dbo.OneIntColumnType");

                SqlParameter[] parameters = new[]
                                            {
                                                new SqlParameter("@EnterpriseId", enterpriseId),
                                                new SqlParameter("@FederalSubjectId", federalSubjectId),
                                                new SqlParameter("@FederalDistrictId", federalDistrictId),
                                                new SqlParameter("@StartDate", date),
                                                new SqlParameter("@EndDate", AddQrtl(date)),
                                                activitiesParameter
                                            };
                List<ConsolidatedDynamicReportItem> consolidatedReportItems =
                    _context.Database.SqlQuery<ConsolidatedDynamicReportItem>(ConsolidateDataStoredProcedureCaller,
                                                                             parameters).ToList();

                consolidatedReportItems.ForEach(x =>
                {
                    x.StartPeriodDate = date;
                    x.EndPeriodDate = AddQrtl(date);
                });
                reportItems.AddRange(consolidatedReportItems);
                date = AddQrtl(date);
            }

            IStatisticDataConverter<ConsolidatedDynamicReportItem> converter =
                _converterFactory.GetConverter<ConsolidatedDynamicReportItem>();
            return converter.Convert(reportItems, criteries);
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
            DataTable verticalItemsTable = CreateOneRowDataTable(verticalItems, "TwoIntColumnType");
            SqlParameter activitiesParameter = CreateSqlParameter(verticalItemsTable, "@activities", "dbo.OneIntColumnType");
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
            else
                returnDate = firstQ.AddYears(1);
            return returnDate;

        }

        public IEnumerable<StatisticData> GetFastSummaryReportData(int enterpriseId, int federalSubjectId, int federalDistrictId, int activityId)
        {
            SqlParameter[] parameters = new[]
                                            {
                                                new SqlParameter("@enterpriseid", enterpriseId),
                                                new SqlParameter("@federalsubjectid", federalSubjectId),
                                                new SqlParameter("@federaldistrictid", federalDistrictId),
                                                new SqlParameter("@activityid", activityId)
                                            };
            IEnumerable<FastSummaryReportItem> fastReportItems =
                _context.Database.SqlQuery<FastSummaryReportItem>(FastSummaryStoredProcedureCaller,
                                                                         parameters);
            IStatisticDataConverter<FastSummaryReportItem> converter =
                _converterFactory.GetConverter<FastSummaryReportItem>();
            fastReportItems = fastReportItems.ToList();
            return converter.Convert(fastReportItems);
        }

        public List<FederalDistrict> GetFederalDistricts()
        {
            return _context.FederalDistricts.ToList();
        }

        public List<FederalSubject> GetFederalSubjects(int districtId)
        {
            return _context.FederalSubjects.Where(x => x.FederalDistrictId == districtId || districtId == 0).ToList();
        }

        public List<Enterprise> GetEnterprises(int subjectId)
        {
            return _context.Enterprises.Where(x => x.FederalSubjectId == subjectId || subjectId == 0).ToList();
        }

        public IEnumerable<Activity> GetActivities()
        {
            return _context.Activities;
        }

        public IEnumerable<FilterCritery> GetConsolidateFilterCriteries()
        {
            return Filters.GetConsolidateCriteries();
        }

        private DataTable CreateOneRowDataTable<T>(IEnumerable<T> items, string typeName)
        {
            DataTable result = new DataTable(typeName);
            result.Columns.Add("Id", typeof(T));
            foreach (T item in items)
            {
                DataRow row = result.NewRow();
                row[0] = item;
                result.Rows.Add(row);
            }
            return result;
        }

        private SqlParameter CreateSqlParameter(DataTable dt, string parameterName, string typeName)
        {
            SqlParameter prm = new SqlParameter(parameterName, SqlDbType.Structured);
            prm.Value = dt;
            prm.TypeName = typeName;
            return prm;
        }

        public IEnumerable<StatisticData> GetSummaryReportData(int enterpiseId, int federalSubjectId, int federalDistrictId, DateTime boundDate, List<int> activities, List<int> genders, List<int> educationLevels, List<int> postLevels)
        {
            DataTable activitiesDataTable = CreateOneRowDataTable(activities, "OneIntColumnType");
            DataTable educationLevelsDataTable = CreateOneRowDataTable(educationLevels, "OneIntColumnType");
            DataTable postLevelsDataTable = CreateOneRowDataTable(postLevels, "OneIntColumnType");
            DataTable gendersDataTable = CreateOneRowDataTable(genders, "OneBitColumnType");

            SqlParameter activitiesParameter = CreateSqlParameter(activitiesDataTable, "activities", "dbo.OneIntColumnType");
            SqlParameter educationLevelsParameter = CreateSqlParameter(educationLevelsDataTable, "educationlevels", "dbo.OneIntColumnType");
            SqlParameter postLevelsParameter = CreateSqlParameter(postLevelsDataTable, "postlevels", "dbo.OneIntColumnType");
            SqlParameter gendersParameter = CreateSqlParameter(gendersDataTable, "genders", "dbo.OneBitColumnType");

            IEnumerable<SummaryReportItem> summaryReportItems = _context.Database.SqlQuery<SummaryReportItem>(SummaryDataStoredProcedureCaller,
                gendersParameter,
                educationLevelsParameter,
                postLevelsParameter,
                new SqlParameter("bounddate", boundDate),
                new SqlParameter("enterpriseid", enterpiseId),
                new SqlParameter("federalsubjectid", federalSubjectId),
                new SqlParameter("federaldistrictid", federalDistrictId),
                activitiesParameter);

            IStatisticDataConverter<SummaryReportItem> converter = _converterFactory.GetConverter<SummaryReportItem>();
            return converter.Convert(summaryReportItems);
        }


        public IEnumerable<PostLevel> PostLevels()
        {
            return _context.PostLevels;
        }

        public IEnumerable<EducationLevel> EducationLevels()
        {
            return _context.EducationLevels;
        }
    }
}

