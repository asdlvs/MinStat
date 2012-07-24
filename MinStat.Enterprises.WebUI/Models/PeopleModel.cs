using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.Enterprises.WebUI.Models
{
    public class PeopleModel
    {
        public IEnumerable<PersonModel> PersonModels { get; set; }

        public string OrderBy { get; set; }

        public string SummaryTitle { get; set; }

        public int SummaryId { get; set; }

        public int Page { get; set; }

        public int Count { get; set; }

        public int PageSize { get; set; }
    }
}