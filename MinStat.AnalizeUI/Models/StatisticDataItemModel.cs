using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.AnalizeUI.Models
{
    public class StatisticDataItemModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<String> Values { get; set; }
        public int BoldLevel { get; set; }
    }
}