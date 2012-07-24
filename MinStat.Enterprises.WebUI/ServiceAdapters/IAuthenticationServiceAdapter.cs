using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinStat.Enterprises.WebUI.ServiceAdapters
{
    public interface IAuthenticationServiceAdapter
    {
        bool Login(string username, string password, string customCredential, bool isPersistent);
        void Logout();
        bool ValidateUser(string username, string password, string customCredential);
    }
}
