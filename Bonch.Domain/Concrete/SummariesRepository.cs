using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.Abstract;
using System.Data.Entity;
using Bonch.Domain.POCO;

namespace Bonch.Domain.Concrete
{
    public class SummariesRepository : ISummariesRepository
    {
        MinStatDbContext _context;
        public SummariesRepository()
        {
            _context = new MinStatDbContext();
        }

        public List<Summary> List()
        {
            return _context.Summaries.AsNoTracking().OrderByDescending(s => s.CreateDate).ToList();
        }

        public Summary Save(Summary summary, List<Activity> activities)
        {
            if (summary.Id == 0)
            {
                _context.Summaries.Add(summary);
            }
            else
            {
                summary = _context.Summaries.Single(x => x.Id == summary.Id);
                summary.SummaryActivities.Clear();
            }
            foreach (var activity in activities)
            {
                SummaryActivity sa = new SummaryActivity 
                {
                    ActivityId = activity.Id,
                    Summary = summary
                };
                _context.SummaryActivities.Add(sa);
            }
            _context.SaveChanges();
            return summary;
        }

        //TODO: как-то по-индусски получилось
        public List<Activity> Activities(Summary summary)
        {
            var result = _context.Activities.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Checked = x.SummaryActivities.Where(xx => xx.SummaryId == summary.Id).Count() > 0
            }).ToArray();

            return result.Select(x => new Activity { Id = x.Id, Title = x.Title, Checked = x.Checked}).ToList();
        }

        public List<Summary> Undelivered()
        {
            return _context.Summaries.Where(s => !s.Published).ToList();
        }
    }
}
