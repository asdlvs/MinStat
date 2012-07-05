using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Bonch.Domain.POCO
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public string Job { get; set; }
        [NotMapped]
        public DateTime BirthDate { get; set; }
        public string EducationLevel { get; set; }
        public string JobLevel { get; set; }
        public int SummaryId { get; set; }
        public int ActivityId { get; set; }
    }
}
