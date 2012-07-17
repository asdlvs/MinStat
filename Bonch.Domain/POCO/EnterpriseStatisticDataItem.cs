using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Bonch.Domain.POCO
{
    public class EnterpriseStatisticDataItem
    {
        [Key]
        [Column("ActivityId", Order = 0)]
        public int ActivityId { get; set; }
        public string ActivityTitle { get; set; }

        [Key]
        [Column("EnterpriseId", Order = 1)]
        public int EnterpriseId { get; set; }
        public string EnterpriseTitle { get; set; }

        [Key]
        [Column("SummaryId", Order = 2)]
        public int SummaryId { get; set; }
        public string SummaryTitle { get; set; }

        [Key]
        [Column("EducationLevelId", Order = 3)]
        public int EducationLevelId { get; set; }
        public string EducationLevelTitle { get; set; }

        [Key]
        [Column("JobLevelId", Order = 4)]
        public int JobLevelId { get; set; }
        public string JobLevelTitle { get; set; }

        public int Count { get; set; }
    }
}
