using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinStat.Enterprises.DAL.POCO
{
    public class FederalSubject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int FederalDistrictId { get; set; }
    }
}
