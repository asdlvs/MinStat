using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.Enterprises.WebUI.Models
{
    public class SummaryModel
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public string CreateDate { get; set; }
        public bool Published { get; set; }
        public string PublishedDate { get; set; }
        public int[] Activities { get; set; }
        public List<ActivityModel> ActivityModels { get; set; }
        public List<PersonModel> PersonModels { get; set; }

    }
}