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
        public IEnumerable<POCO.StatisticDataItem> GetItems(int enterpriseId, DateTime startDate, DateTime endDate)
        {
            var parameters = new SqlParameter[] { 
                new SqlParameter("@EnterpriseId", enterpriseId),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };

            var result = context.Database.SqlQuery<StatisticDataItem>("Exec dbo.GetStatisticDataByEnterprise @EnterpriseId, @StartDate, @EndDate", parameters);
            var data = result.ToList<StatisticDataItem>();
            return data;
        }


        public IEnumerable<POCO.Activity> GetActivities()
        {
            return context.Activities;
        }
    }
}
