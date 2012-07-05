// -----------------------------------------------------------------------
// <copyright file="MinStatIdentity.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Bonch.Security
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Security.Principal;
  using System.Text;
  using Bonch.Domain.POCO;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public class MinStatIdentity : User, IIdentity
  {
    public string AuthenticationType
    {
      get { throw new NotImplementedException(); }
    }

    public bool IsAuthenticated
    {
      get { throw new NotImplementedException(); }
    }

    public string Name
    {
      get { throw new NotImplementedException(); }
    }


  }
}
