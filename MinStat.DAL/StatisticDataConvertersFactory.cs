using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinStat.DAL.POCO.ReportItems;
using MinStat.DAL.Converters;

namespace MinStat.DAL
{
    public class StatisticDataConvertersFactory: IStatisticDataConvertersFactory
    {
        public IStatisticDataConverter<T> GetConverter<T>()
        {
            if (typeof(T) == typeof(ConsolidatedReportItem))
            {
                return (IStatisticDataConverter<T>) new ConsolidatedReportConverter();
            }
            if (typeof(T) == typeof(FullReportItem))
            {
                return (IStatisticDataConverter<T>) new FullReportConverter();
            }
            if (typeof(T) == typeof(PersonaliesReportItem))
            {
                return (IStatisticDataConverter<T>) new PersonaliesReportConverter();
            }
            if (typeof(T) == typeof(SelectionQtyStaticReportItem))
            {
                return (IStatisticDataConverter<T>)new SelectionQtyStaticConverter();
            }
            if (typeof(T) == typeof(SelectionQtyDynamicReportItem))
            {
                return (IStatisticDataConverter<T>)new SelectionQtyDynamicConverter();
            }
            throw new ArgumentException("There is not converter for such type.");
        }
    }
}
