using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bonch.WebUI.Models
{
    public class PersonViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salary { get; set; }
        public string Job { get; set; }
        public string Age { get; set; }
        public string EducationLevel { get; set; }
        public string JobLevel { get; set; }
    }
}