using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Bonch.Domain.POCO;
using System.ComponentModel.DataAnnotations;

namespace Bonch.Domain.Concrete
{
    public class MinStatDbContext : DbContext
    {
        public MinStatDbContext()
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Summary> Summaries
        { get; set; }

        public DbSet<Activity> Activities
        { get; set; }

        public DbSet<SummaryActivity> SummaryActivities
        { get; set; }

        public DbSet<Person> Peoples
        { get; set; }

        public DbSet<Enterprise> Enterprises
        { get; set; }

        public DbSet<User> Users
        { get; set; }

        public DbSet<JobLevel> JobLevels { get; set; }

        public DbSet<EducationLevel> EducationLevels { get; set; }

        public DbSet<StatisticDataItem> StatisticDataItems { get; set; }
    }
}
