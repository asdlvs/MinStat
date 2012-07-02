using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bonch.WebUI.Models
{
    public class SummaryViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CreateDate { get; set; }
        public string AuthorName { get; set; }
        public string Published { get; set; }
        public string PersonsCount { get; set; }
    }
}