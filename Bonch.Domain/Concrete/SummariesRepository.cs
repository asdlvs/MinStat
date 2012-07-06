using System.Collections.Generic;
using System.Linq;
using Bonch.Domain.Abstract;
using Bonch.Domain.POCO;

namespace Bonch.Domain.Concrete
{
  using System.Transactions;

  using Bonch.Domain.Comparers;

  public class SummariesRepository : ISummariesRepository
  {
    MinStatDbContext _context;
    public SummariesRepository()
    {
      _context = new MinStatDbContext();
    }

    public List<Summary> List()
    {
      return _context.Summaries.AsNoTracking().OrderByDescending(s => s.Id).ToList();
    }

    public Summary Save(Summary summary, List<Activity> activities)
    {
      if (summary.Published)
        return summary;

      using (TransactionScope ts = new TransactionScope())
      {
        if (summary.Id == 0)
        {
          _context.Summaries.Add(summary);
        }
        else
        {
          summary = _context.Summaries.Single(x => x.Id == summary.Id);
          var oldActivities =
            _context.Summaries
              .Include("SummaryActivities")
              .Include("SummaryActivities.Activity")
              .Single(x => x.Id == summary.Id)
              .SummaryActivities
              .Select(x => x.Activity)
              .ToList();

          var activitiesToRemove = oldActivities.Except(activities, new ActivityEqualityComparer()).ToList();
          var activitiesToAdd = activities.Except(oldActivities, new ActivityEqualityComparer()).ToList();
          activities = activitiesToAdd.ToList();
          foreach (var activityToRemove in activitiesToRemove)
          {
            var summaryActivitiesToRemove =
              _context.SummaryActivities.Single(x => x.ActivityId == activityToRemove.Id && x.SummaryId == summary.Id);
            _context.SummaryActivities.Remove(summaryActivitiesToRemove);
          }
        }
        foreach (var activity in activities)
        {
          SummaryActivity sa = new SummaryActivity { ActivityId = activity.Id, Summary = summary };
          _context.SummaryActivities.Add(sa);
        }
        _context.SaveChanges();
        ts.Complete();
        return summary;
      }
    }

    //TODO: как-то по-индусски получилось
    public List<Activity> Activities(Summary summary)
    {
      var result = _context.Activities.Select(x => new
      {
        Id = x.Id,
        Title = x.Title,
        Checked = x.SummaryActivities.Any(xx => xx.SummaryId == summary.Id)
      }).ToArray();

      return result.Select(x => new Activity { Id = x.Id, Title = x.Title, Checked = x.Checked }).ToList();
    }

    public List<Summary> Undelivered()
    {
      return _context.Summaries.Where(s => !s.Published).ToList();
    }


    public void Publish(int summaryId)
    {
      Summary summary = _context.Summaries.Single(x => x.Id == summaryId);
      summary.Published = true;
      _context.SaveChanges();
    }

    public Summary Copy(Summary oldSummary, Summary newSummary)
    {
      var activities = oldSummary.SummaryActivities.Select(x => x.Activity);
      return this.Save(newSummary, activities.ToList());
    }
  }
}
