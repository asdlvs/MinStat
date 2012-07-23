// -----------------------------------------------------------------------
// <copyright file="EnterpriseIdentifier.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MinStat.Enterprises.DAL
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
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
      if(!_context.Users.Any(x => x.Mail == username)) { throw new ArgumentException("Wrong username");}
      #endregion

      return _context.Users.First(x => x.Mail == username).EnterpriseId;
    }
  }
}
