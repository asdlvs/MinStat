using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Bonch.Domain.POCO
{
    public class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [InverseProperty("Activity")]
        public virtual ICollection<SummaryActivity> SummaryActivities { get; set; }

        [NotMapped]
        public bool Checked { get; set; }    

    }
}
