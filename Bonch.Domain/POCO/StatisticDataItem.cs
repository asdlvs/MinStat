using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Bonch.Domain.POCO
{
    public class StatisticDataItem
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int Part_1 { get; set; }
        public int Part_2 { get; set; }
        public int Part_3 { get; set; }
        public int Part_4 { get; set; }
        public int Part_5 { get; set; }

        public int j1_e1 { get; set; }
        public int j1_e2 { get; set; }
        public int j1_e3 { get; set; }
        public int j2_e1 { get; set; }
        public int j2_e2 { get; set; }
        public int j2_e3 { get; set; }
        public int j3_e1 { get; set; }
        public int j3_e2 { get; set; }
        public int j3_e3 { get; set; }
        public int j4_e1 { get; set; }
        public int j4_e2 { get; set; }
        public int j4_e3 { get; set; }
    }
}
