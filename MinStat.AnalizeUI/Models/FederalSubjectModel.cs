using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.AnalizeUI.Models
{
    public class FederalSubjectModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int FederalDistrictId { get; set; }
    }
}