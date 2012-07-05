using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.POCO;

namespace Bonch.Domain.Abstract
{
    public interface IEnterpriseRegistrator
    {
        IEnumerable<Enterprise> GetEnterprises();

        IEnumerable<User> GetUsers(Enterprise enterprise);

        void CreateUser(User user, Enterprise enterprise);

        void DeleteUser(User user, Enterprise enterprise);
    }
}
