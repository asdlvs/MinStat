using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL.Converters
{
    class PersonaliesReportConverter : IStatisticDataConverter<PersonaliesReportConverter>
    {
        public IEnumerable<StatisticData> Convert(IEnumerable<PersonaliesReportConverter> result)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<StatisticData> Convert(IEnumerable<PersonaliesReportConverter> result, List<int> criteries)
        {
          throw new NotImplementedException();
        }
    }
}
