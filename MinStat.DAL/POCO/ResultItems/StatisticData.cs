using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinStat.DAL.POCO.ResultItems
{
    [Serializable]
    public class StatisticData
    {
        public Dictionary<string, string> Titles { get; set; }

        public List<StatisticDataItem> Lines { get; set; }
    }
}
