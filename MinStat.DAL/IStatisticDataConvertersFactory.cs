using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinStat.DAL.Converters;

namespace MinStat.DAL
{
    public interface IStatisticDataConvertersFactory
    {
        IStatisticDataConverter<T> GetConverter<T>();
    }
}
