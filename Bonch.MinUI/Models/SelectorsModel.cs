using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bonch.Domain.POCO;

namespace Bonch.MinUI.Models
{
    public class SelectorsViewModel
    {
        public List<FederalDistrict> Districts { get; set; }

        public List<FederalSubject> Subjects { get; set; }

        public List<Enterprise> Enterprises { get; set; }
    }
}