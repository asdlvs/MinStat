// -----------------------------------------------------------------------
// <copyright file="DatabaseContext.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MinStat.Enterprises.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using MinStat.Enterprises.DAL.POCO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Summary> Summaries { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<SummaryActivity> SummaryActivities { get; set; }

        public DbSet<Enterprise> Enterprises { get; set; }

        public DbSet<FederalSubject> FederalSubjects { get; set; }

        public DbSet<FederalDistrict> FederalDistricts { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<EducationLevel> EducationLevels { get; set; }

        public DbSet<PostLevel> PostLevels { get; set; }

    }
}
