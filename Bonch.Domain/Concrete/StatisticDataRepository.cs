using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.Abstract;

namespace Bonch.Domain.Concrete
{
    public class StatisticDataRepository : IStatisticDataRepository
    {
        MinStatDbContext context;
        public StatisticDataRepository()
        {
            context = new MinStatDbContext();
        }
        public IEnumerable<POCO.EnterpriseStatisticDataItem> GetItems()
        {
            return context.EnterpriseStatisticDataItems;
        }


        public IEnumerable<POCO.Activity> GetActivities()
        {
            return context.Activities;
        }
    }
}
