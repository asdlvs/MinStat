using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.POCO;

namespace Bonch.Security.Abstract
{

    public interface IUserRepository
    {
        User GetUser(string username);

        void SetUser(User user);

    }
}
