using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.AnalizeUI.Models
{
    public class StatisticDataItemModel
    {
        public string Title { get; set; }
        public IEnumerable<String> Values { get; set; }
        public int BoldLevel { get; set; }
    }
}