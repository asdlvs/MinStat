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
  using System.Text;
  using Bonch.Domain.POCO;
  using System.Security.Principal;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public class MinStatIdentity : User, IIdentity
  {
    public string AuthenticationType
    {
      get
      {
        return  "custom";
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
  }
}
