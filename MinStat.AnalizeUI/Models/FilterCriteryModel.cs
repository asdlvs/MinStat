using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.AnalizeUI.Models
{
    public class FilterCriteryModel
    {
        public KeyValuePair<int, string> Key;
        public string KeyValue;
        public Dictionary<int, string> Inner { get; set; }
    }
}