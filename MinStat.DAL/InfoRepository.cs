using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinStat.DAL.POCO;

namespace MinStat.DAL
{
    public class InfoRepository : IInfoRepository
    {
        private IUserNotifier _notifier;
        private DatabaseContext _context;

        public InfoRepository(IUserNotifier notifier)
        {
            _notifier = notifier;
            _context = new DatabaseContext();
        }

        public InfoRepository()
            : this(new UserNotifier())
        {
        }

        public void CreateEnterprise(string enterpriseTitle, int federalSubjectId, string mail)
        {
            #region Pre-Conditions
            if(!_context.FederalSubjects.Any(x => x.Id == federalSubjectId)) { throw new ArgumentException("wrong federal subject id");}
            #endregion
            Enterprise newEnterprise = new Enterprise { Title = enterpriseTitle, FederalSubjectId = federalSubjectId };
            _context.Enterprises.Add(newEnterprise);
            _context.SaveChanges();
            _notifier.Notify(mail, newEnterprise.Id);
        }


        public void RemoveEnterprise(int enterpriseId)
        {
            var summaries = _context.Summaries.Where(x => x.EnterpriseId == enterpriseId).ToList();
            foreach(var summary in summaries)
            {
                var people = _context.People.Where(x => x.SummaryId == summary.Id).ToList();
                foreach(var person in people)
                {
                    _context.People.Remove(person);
                }
                _context.SaveChanges();
                var summaryActivities = _context.SummaryActivities.Where(x => x.SummaryId == summary.Id).ToList();
                foreach(var summaryActivity in summaryActivities)
                {
                    _context.SummaryActivities.Remove(summaryActivity);
                }
                _context.SaveChanges();
                _context.Summaries.Remove(summary);
                _context.SaveChanges();
            }

            _context.Enterprises.Remove(_context.Enterprises.Single(x => x.Id == enterpriseId));

            _context.SaveChanges();

        }
    }
}
