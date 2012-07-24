// -----------------------------------------------------------------------
// <copyright file="EnterpriseDataRepository.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MinStat.Enterprises.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using MinStat.Enterprises.DAL.Extensions;
    using MinStat.Enterprises.DAL.POCO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EnterpriseDataRepository : IEnterpriseDataRepository
    {
        private DatabaseContext _context;

        public EnterpriseDataRepository()
        {
            _context = new DatabaseContext();
        }

        public void CreateSummary(int enterpriseId, string title, List<int> activitiesIds)
        {
            #region Pre-conditions
            if (title == null) { throw new ArgumentNullException("title"); }
            if (activitiesIds == null) { throw new ArgumentNullException("activitiesIds"); }
            if (!_context.Enterprises.Any(x => x.Id == enterpriseId)) { throw new ArgumentException("Wrong enterprise id", enterpriseId.ToString(CultureInfo.InvariantCulture)); }
            #endregion

            Summary newSummary = new Summary();
            newSummary.CreateDate = DateTime.Now;
            newSummary.EnterpriseId = enterpriseId;
            newSummary.Published = false;
            newSummary.Title = title;
            _context.Summaries.Add(newSummary);
            foreach (int activity in activitiesIds)
            {
                _context.SummaryActivities.Add(new SummaryActivity { SummaryId = newSummary.Id, ActivityId = activity });
            }
            _context.SaveChanges();
        }

        public void CopySummary(int enterpriseId, string title, int summaryId)
        {
            #region Pre-conditions
            if (title == null) { throw new ArgumentNullException("title"); }
            if (!_context.Summaries.Any(x => x.Id == summaryId && x.EnterpriseId == enterpriseId)) { throw new ArgumentException("Wrong summary id", summaryId.ToString(CultureInfo.InvariantCulture)); }
            #endregion

            Summary summaryTo = new Summary();
            summaryTo.Title = title;
            summaryTo.CreateDate = DateTime.Now;
            summaryTo.Published = false;
            summaryTo.EnterpriseId = enterpriseId;
            _context.Summaries.Add(summaryTo);

            Summary summaryFrom = _context.Summaries.First(x => x.Id == summaryId);
            IEnumerable<SummaryActivity> summaryActivities =
              _context.SummaryActivities.Where(x => x.SummaryId == summaryFrom.Id);
            foreach (SummaryActivity summaryActivity in summaryActivities)
            {
                _context.SummaryActivities.Add(new SummaryActivity { SummaryId = summaryTo.Id, ActivityId = summaryActivity.ActivityId });
            }
            IEnumerable<Person> people = _context.People.Where(x => x.SummaryId == summaryFrom.Id);
            foreach (var person in people)
            {
                if (person.DismissalYear != null)
                {
                    Person newPerson = new Person
                      {
                          BirthYear = person.BirthYear,
                          ActivityId = person.ActivityId,
                          DismissalYear = person.DismissalYear,
                          EducationLevelId = person.EducationLevelId,
                          Gender = person.Gender,
                          HiringYear = person.HiringYear,
                          Post = person.Post,
                          PostLevelId = person.PostLevelId,
                          StartPostYear = person.StartPostYear,
                          SummaryId = summaryTo.Id,
                          Title = person.Title,
                          WasQualificationIncrease = person.WasQualificationIncrease,
                          WasValidate = person.WasValidate,
                          YearSalary = person.YearSalary
                      };
                    _context.People.Add(newPerson);
                }
            }
            _context.SaveChanges();
        }

        public void RemoveSummary(int summaryId)
        {
            #region Pre-conditions
            if(!_context.Summaries.Any(x => x.Id == summaryId)) { throw new ArgumentException("wrong summary id");}
            if(_context.Summaries.First(x => x.Id == summaryId).Published) { throw new InvalidOperationException("Cannot remove published summary");}
            #endregion

            IEnumerable<Person> people = _context.People.Where(x => x.SummaryId == summaryId);
            foreach (var person in people)
            {
                _context.People.Remove(person);
            }
            IEnumerable<SummaryActivity> summaryActivities = _context.SummaryActivities.Where(x => x.SummaryId == summaryId);
            foreach (var summaryActivity in summaryActivities)
            {
                _context.SummaryActivities.Remove(summaryActivity);
            }
            Summary summary = _context.Summaries.First(x => x.Id == summaryId);
            _context.Summaries.Remove(summary);

            _context.SaveChanges();
        }

        public void AddActivities(int summaryId, List<int> activitiesIds)
        {
            #region Pre-conditions
            if (!_context.Summaries.Any(x => x.Id == summaryId)) { throw new ArgumentException("Wrong summary Id", summaryId.ToString()); }
            #endregion

            foreach (int activitiesId in activitiesIds)
            {
                if (_context.Activities.Any(x => x.Id == activitiesId) && !_context.SummaryActivities.Any(x => x.ActivityId == activitiesId && x.SummaryId == summaryId))
                {
                    _context.SummaryActivities.Add(new SummaryActivity { SummaryId = summaryId, ActivityId = activitiesId });
                }
            }
            _context.SaveChanges();
        }

        public void RemoveActivity(int summaryId, int activityId)
        {
            #region Pre-conditions
            if (!_context.Summaries.Any(x => x.Id == summaryId)) { throw new ArgumentException("Wrong summary Id", summaryId.ToString()); }
            if (!_context.Activities.Any(x => x.Id == activityId)) { throw new ArgumentException("Wrong activity Id", activityId.ToString()); }
            #endregion

            SummaryActivity summaryActivity;
            if ((summaryActivity = _context.SummaryActivities.FirstOrDefault(x => x.ActivityId == activityId && x.SummaryId == summaryId)) != null)
            {
              var people = _context.People.Where(x => x.ActivityId == activityId && x.SummaryId == summaryId);
              foreach (var person in people)
              {
                _context.People.Remove(person);
              }
                _context.SummaryActivities.Remove(summaryActivity);
            }
        }

      public IEnumerable<Person> GetPeople(int summaryId, int size, int offset, string orderby)
      {
        return _context.People.Where(x => x.SummaryId == summaryId).OrderBy(x => x.Id).OrderBy(orderby).Skip(offset).Take(size);
      }

      public void CreatePerson(int summaryId, int activityId, string title, string post, int postLevelId, int educationLevelId, decimal yearSalary, bool gender, bool wasQualificationIncrease,
          bool wasValidate, int birthYear, int hiringYear, int startPostYear, int dismissalYear)
        {
            #region Pre-conditions
            if (!_context.Summaries.Any(x => x.Id == summaryId)) { throw new ArgumentException("Wrong summary Id", summaryId.ToString()); }
            if (!_context.Activities.Any(x => x.Id == activityId)) { throw new ArgumentException("Wrong activity Id", activityId.ToString()); }
            if (!_context.EducationLevels.Any(x => x.Id == educationLevelId)) { throw new ArgumentException("Wrong education level id", educationLevelId.ToString()); }
            if (!_context.PostLevels.Any(x => x.Id == postLevelId)) { throw new ArgumentException("Wrong post level Id", postLevelId.ToString()); }
            if (!_context.SummaryActivities.Any(x => x.ActivityId == activityId && x.SummaryId == summaryId)) { throw new ArgumentException("No such SummaryActivity"); }
            #endregion

            Person person = new Person
              {
                  ActivityId = activityId,
                  BirthYear = birthYear,
                  DismissalYear = dismissalYear,
                  EducationLevelId = educationLevelId,
                  Gender = gender,
                  HiringYear = hiringYear,
                  Post = post,
                  PostLevelId = postLevelId,
                  StartPostYear = startPostYear,
                  SummaryId = summaryId,
                  Title = title,
                  WasQualificationIncrease = wasQualificationIncrease,
                  WasValidate = wasValidate,
                  YearSalary = yearSalary
              };
            _context.People.Add(person);
        }

        public void RemovePerson(int personId)
        {
            Person person;
            if ((person = _context.People.FirstOrDefault(x => x.Id == personId)) != null)
            {
                _context.People.Remove(person);
            }
        }

        public IEnumerable<Person> FindPerson(string searchText)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(int personId, string title, string post, int postLevelId, int educationLevelId, decimal yearSalary, bool gender, bool wasQualificationIncrease,
          bool wasValidate, int birthYear, int hiringYear, int startPostYear, int dismissalYear)
        {
            #region Pre-conditions
            if (!_context.People.Any(x => x.Id == personId)) { throw new ArgumentException("wrong perons id", personId.ToString()); }
            if (!_context.EducationLevels.Any(x => x.Id == educationLevelId)) { throw new ArgumentException("Wrong education level id", educationLevelId.ToString()); }
            if (!_context.PostLevels.Any(x => x.Id == postLevelId)) { throw new ArgumentException("Wrong post level Id", postLevelId.ToString()); }
            #endregion

            Person person = _context.People.First(x => x.Id == personId);
            person.BirthYear = birthYear;
            person.HiringYear = hiringYear;
            person.DismissalYear = dismissalYear;
            person.EducationLevelId = educationLevelId;
            person.Gender = gender;
            person.Post = post;
            person.PostLevelId = postLevelId;
            person.StartPostYear = startPostYear;
            person.Title = title;
            person.WasQualificationIncrease = wasQualificationIncrease;
            person.WasValidate = wasValidate;
            person.YearSalary = yearSalary;
            _context.SaveChanges();
        }

        public IEnumerable<Summary> GetSummaries(int enterpriseId)
        {

            return _context.Summaries
                //.Include("SummaryActivities")
                //.Include("SummaryActivities.Activity")
                .Where(x => x.EnterpriseId == enterpriseId);
        }


        public IEnumerable<Activity> GetActivities()
        {
            return _context.Activities;
        }

        public IEnumerable<Activity> GetActivities(int summaryId)
        {
            return _context.Activities.Where(x => x.SummaryActivities.Any(xx => xx.SummaryId == summaryId));
        }


        public void PublishSummary(int summaryId)
        {
            Summary summary = _context.Summaries.First(x => x.Id == summaryId);
            summary.Published = true;
            summary.PublishedDate = DateTime.Now;
            _context.SaveChanges();
        }


        public void UpdateSummary(int summaryId, string title, List<int> activities)
        {
          #region Pre-conditions
          if(!_context.Summaries.Any(x => x.Id == summaryId)) {throw new ArgumentException("Wrong summary id");}
          #endregion

          Summary summary = _context.Summaries.First(x => x.Id == summaryId);
          IEnumerable<int> summaryActivities =
            _context.SummaryActivities.Where(x => x.SummaryId == summaryId).Select(x => x.ActivityId).ToList();
          IEnumerable<int> activitiesToDelete = summaryActivities.Except(activities);
          foreach (int activityToDelete in activitiesToDelete)
          {
            this.RemoveActivity(summaryId, activityToDelete);
          }

          IEnumerable<int> activitiesToAdd = activities.Except(summaryActivities);
            this.AddActivities(summaryId, activitiesToAdd.ToList());

          if(!String.IsNullOrWhiteSpace(title) && !summary.Title.Equals(title))
          {
            summary.Title = title;
          }

          _context.SaveChanges();
        }


        public int GetPeopleArraySize(int summaryId)
        {
          return this._context.People.Count(x => x.SummaryId == summaryId);
        }
    }
}
