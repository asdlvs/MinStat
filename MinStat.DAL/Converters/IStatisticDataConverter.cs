using System.Collections.Generic;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL.Converters
{
    public interface IStatisticDataConverter<T>
    {
        IEnumerable<StatisticData> Convert(IEnumerable<T> result);
    }
}
