using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.POCO;
using System.Security.Principal;
namespace Bonch.Security
{

    public class MinStatIdentity : User, IIdentity
    {
        public string AuthenticationType
        {
            get
            {
                return "custom";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return this.Id != 0;
            }
        }

        public string Name
        {
            get
            {
                return this.Mail;
            }
        }

        public string FullName
        {
            get { return String.Format("{0} {1}", this.FirstName, this.LastName); }
        }

        public bool IsDataFilled()
        {
            return !String.IsNullOrEmpty(this.FirstName)
                && !String.IsNullOrEmpty(this.LastName)
                && !String.IsNullOrEmpty(this.Phone); 
        }
    }
}
