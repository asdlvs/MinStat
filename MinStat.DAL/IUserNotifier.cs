using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinStat.DAL
{
    public interface IUserNotifier
    {
        void Notify(string mail, int emterpriseId);
        void CreateUser(string mail, string pwd, int enterpriseId);
    }
}
