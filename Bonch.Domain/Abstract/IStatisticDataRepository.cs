using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.POCO;

namespace Bonch.Domain.Abstract
{
    public interface IStatisticDataRepository
    {
        IEnumerable<StatisticDataItem> GetItems(int id, DateTime startDate, DateTime endDate, AreaType type);

        IEnumerable<Activity> GetActivities();
    }
}
