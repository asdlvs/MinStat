using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinStat.DAL.POCO
{
    [Serializable]
    public class Activity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Part_1 { get; set; }

        public int Part_2 { get; set; }

        public int Part_3 { get; set; }

        public int Part_4 { get; set; }

        public int Part_5 { get; set; }
    }
}
