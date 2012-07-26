
using System;
using System.ComponentModel.DataAnnotations;

namespace MinStat.DAL.POCO
{
    [Serializable]
    public class SummaryActivity
    {
        [Key]
        [Column("SummaryId", Order = 0)]
        public int SummaryId { get; set; }



        [Key]
        [Column("ActivityId", Order = 1)]
        public int ActivityId { get; set; }

    }
}
