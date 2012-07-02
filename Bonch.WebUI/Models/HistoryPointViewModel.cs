using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bonch.WebUI.Models
{
    public class HistoryPointViewModel
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string ActionType { get; set; }
        public string Description { get; set; }
        public string CustomDescription { get; set; }
        public string Remember { get; set; }
        public string Author { get; set; }
    }
}