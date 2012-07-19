using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.Abstract;
using Bonch.Domain.POCO;
using System.Data.SqlClient;

namespace Bonch.Domain.Concrete
{
    public class StatisticDataRepository : IStatisticDataRepository
    {
        MinStatDbContext context;
        public StatisticDataRepository()
        {
            context = new MinStatDbContext();
        }
        public IEnumerable<POCO.StatisticDataItem> GetItems(int id, DateTime startDate, DateTime endDate, AreaType type)
        {
            var parameters = new SqlParameter[] { 
                new SqlParameter("@Id", id),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };

            string procedureName = String.Empty;
            switch (type)
            { 
                case AreaType.Enterprise:
                    procedureName = "GetStatisticDataByEnterprise";
                    break;
                case AreaType.Subject:
                    procedureName = "GetStatisticDataBySubject";
                    break;
                case AreaType.District:
                    procedureName = "GetStatisticDataByDistrict";
                    break;
            }
            var result = context.Database.SqlQuery<StatisticDataItem>(String.Format("Exec dbo.{0} @Id, @StartDate, @EndDate", procedureName), parameters);
            var data = result.ToList<StatisticDataItem>();
            return data;
        }


        public IEnumerable<POCO.Activity> GetActivities()
        {
            return context.Activities;
        }
    }
}
