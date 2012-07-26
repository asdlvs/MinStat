using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.AnalizeUI.Models
{
    public class CreateEnterpriseModel
    {
        public int FederalDistrictId { get; set; }

        public int FederalSubjectId { get; set; }

        public string Title { get; set; }

        public string Mail { get; set; }

        public IEnumerable<EnterpriseModel> Enterprises { get; set; }
    }
}