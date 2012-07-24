namespace MinStat.Enterprises.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EnterpriseIdentifier : IIdentifier
    {
        private DatabaseContext _context;

        public EnterpriseIdentifier()
        {
            _context = new DatabaseContext();
        }

        public int EnterpriseId(string username)
        {
            #region Pre-conditions
            if (!_context.Users.Any(x => x.Mail == username)) { throw new ArgumentException("Wrong username"); }
            #endregion

            return _context.Users.First(x => x.Mail == username).EnterpriseId;
        }

        public bool Validate(string username, string password)
        {
            #region Pre-conditions
            if (String.IsNullOrWhiteSpace(username)) { throw new ArgumentException(username); }
            if (String.IsNullOrWhiteSpace(password)) { throw new ArgumentException(password); }
            #endregion

            return _context.Users.Any(x => x.Mail == username && x.Password == password);
        }
    }
}
