using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bonch.MinStat.ImportFileGenerator
{
    public class Person
    {
        public string Title { get; set; }
        public int EducationLevelId { get; set; }
        public int PostLevelId { get; set; }
        public string Post { get; set; }
        public decimal YearSalary { get; set; }
        public bool Gender { get; set; }
        public bool WasIncrease { get; set; }
        public bool WasValidation { get; set; }
        public int BirthYear { get; set; }
        public int HiringYear { get; set; }
        public int StartPostYear { get; set; }
        public int DismissalYear { get; set; }
    }
}
