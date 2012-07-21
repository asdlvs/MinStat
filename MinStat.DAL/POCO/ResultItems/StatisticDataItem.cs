using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinStat.DAL.POCO.ResultItems
{
    [Serializable]
    public class StatisticDataItem
    {
        public string Title { get; set; }
        public List<String> Values { get; set; }
        public int StrongLevel { get; set; }
    }
}
