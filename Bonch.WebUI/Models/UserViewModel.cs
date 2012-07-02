using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bonch.WebUI.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string EnterpriseId { get; set; }
        public string EnterpriseName { get; set; }
    }
}