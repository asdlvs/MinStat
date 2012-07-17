using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Bonch.Domain.POCO
{
    public class StatisticDataItem
    {
        [Key]
        [Column("ActivityId", Order = 0)]
        public int ActivityId { get; set; }
        public string Activity { get; set; }
        public int Part_1 { get; set; }
        public int Part_2 { get; set; }
        public int Part_3 { get; set; }
        public int Part_4 { get; set; }
        public int Part_5 { get; set; }
        [Key]
        [Column("EducationLevelId", Order = 1)]
        public int EducationLevelId { get; set; }
        public string EducationLevel { get; set; }
        [Key]
        [Column("JobLevelId", Order = 2)]
        public int JobLevelId { get; set; }
        public string JobLevel { get; set; }
        public int Count { get; set; }
    }
}
