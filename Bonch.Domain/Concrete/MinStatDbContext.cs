﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Bonch.Domain.POCO;

namespace Bonch.Domain.Concrete
{
    public class MinStatDbContext : DbContext
    {
        public DbSet<Summary> Summaries
        { get; set; }

        public DbSet<Activity> Activities
        { get; set; }

        public DbSet<SummaryActivity> SummaryActivities
        { get; set; }

        public DbSet<Person> Peoples
        { get; set; }
    }
}