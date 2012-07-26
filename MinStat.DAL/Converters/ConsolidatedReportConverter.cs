using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MinStat.DAL.POCO.ReportItems;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL.Converters
{
    class ConsolidatedReportConverter : IStatisticDataConverter<ConsolidatedReportItem>
    {
        //TODO: Неправильно считается
        public IEnumerable<StatisticData> Convert(IEnumerable<ConsolidatedReportItem> result)
        {
            return null;
        }

        private string GetPersentage(int a, int b)
        {
            return string.Format("{0}%", ((decimal)a / (decimal)b) * 100);
        }
    }
}
