using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinStat.DAL.POCO.ReportItems;
using MinStat.DAL.POCO;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL
{
    public interface IStatisticDataRepository
    {
        IEnumerable<StatisticData> GetConsolidatedReportData(
            int enterpriseId, 
            int federalSubjectId, 
            int federalDistrictId, 
            int subsectorId, 
            DateTime startDate, 
            DateTime endDate);

        IEnumerable<StatisticData> GetFullReportData(int enterpriseId,
            int federalSubjectId,
            int federalDistrictId,
            DateTime startDate,
            DateTime endDate);

        IEnumerable<StatisticData> GetPeronaliesReportItem();
    }
}
