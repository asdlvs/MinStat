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
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Summary> Summaries { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SummaryActivity> SummaryActivities { get; set; }
    }
}
