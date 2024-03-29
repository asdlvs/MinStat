﻿
using System;
using System.ComponentModel.DataAnnotations;

namespace MinStat.Enterprises.DAL.POCO
{
    [Serializable]
    public class SummaryActivity
    {
        [Key]
        [Column("SummaryId", Order = 0)]
        [ForeignKey("Summary")]
        public int SummaryId { get; set; }
        [InverseProperty("SummaryActivities")]
        public virtual Summary Summary { get; set; }

        [Key]
        [Column("ActivityId", Order = 1)]
        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        [InverseProperty("SummaryActivities")]
        public virtual Activity Activity { get; set; }
    }
}
