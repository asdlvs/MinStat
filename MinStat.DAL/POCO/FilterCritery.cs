using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinStat.DAL.POCO
{
    [Serializable]
    public class FilterCritery
    {
        public KeyValuePair<int, string> Key;
        public string KeyValue;
        public Dictionary<int, string> Inner { get; set; }
    }
}
