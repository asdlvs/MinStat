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
            return _context.Peoples.Where(x => x.ActivityId == activity.Id && x.SummaryId == summary.Id);
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
                personToUpdate.EducationLevel = person.EducationLevel;
                personToUpdate.JobLevel = person.JobLevel;
            }
            _context.SaveChanges();
        }

        public void Delete(POCO.Person person)
        {
            var personToDelete = _context.Peoples.Single(x => x.Id == person.Id);
            _context.Peoples.Remove(personToDelete);
            _context.SaveChanges();
        }
    }
}
