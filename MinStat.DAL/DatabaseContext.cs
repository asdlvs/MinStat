using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using MinStat.DAL.POCO;

namespace MinStat.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<FederalDistrict> FederalDistricts { get; set; }
        public DbSet<FederalSubject> FederalSubjects { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
    }
}
