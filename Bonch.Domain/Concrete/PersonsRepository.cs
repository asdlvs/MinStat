using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.Abstract;
using Bonch.Domain.POCO;

namespace Bonch.Domain.Concrete
{
    public class PersonsRepository : IPersonsRepository
    {
        MinStatDbContext _context;
        public PersonsRepository()
        {
            _context = new MinStatDbContext();
        }
        public IEnumerable<POCO.Person> List(Summary summary, Activity activity)
        {
            var result = _context.Peoples.Where(x => x.ActivityId == activity.Id && x.SummaryId == summary.Id);
          return result;
        }

        public void Set(POCO.Person person)
        {
            if (person.Id == 0)
            {
              _context.Peoples.Add(person);
            }
            else
            {
                var personToUpdate = _context.Peoples.Single(x => x.Id == person.Id);
                personToUpdate.FirstName = person.FirstName;
                personToUpdate.LastName = person.LastName;
                personToUpdate.Job = person.Job;
                personToUpdate.Salary = person.Salary;
                personToUpdate.BirthDate = person.BirthDate;
                personToUpdate.EducationLevelId = person.EducationLevelId;
                personToUpdate.JobLevelId = person.JobLevelId;
            }
            _context.SaveChanges();
        }

        public void Delete(POCO.Person person)
        {
            var personToDelete = _context.Peoples.Single(x => x.Id == person.Id);
            _context.Peoples.Remove(personToDelete);
            _context.SaveChanges();
        }


        public Person Get(int id)
        {
          return _context.Peoples.Include("Summary").Include("Activity").Single(x => x.Id == id);
        }


        public IEnumerable<JobLevel> JobLevels()
        {
          return _context.JobLevels;
        }

        public IEnumerable<EducationLevel> EducationLevels()
        {
          return _context.EducationLevels;
        }
    }
}
