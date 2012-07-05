using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Security.Abstract;
using Bonch.Domain.POCO;
using Bonch.Domain.Concrete;

namespace Bonch.Security.Concrete
{
    public class UserRepository : IUserRepository
    {
        MinStatDbContext context;
        public UserRepository()
        {
            context = new MinStatDbContext();
        }
        public User GetUser(string username)
        {
            return context.Users.SingleOrDefault(u => u.Mail.Equals(username));
        }

        public void SetUser(User user)
        {
            User userToSet = GetUser(user.Login);
            if (userToSet != null)
            {
                userToSet.FirstName = user.FirstName;
                userToSet.LastName = user.LastName;
                userToSet.Phone = user.Phone;
            }
            else
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
