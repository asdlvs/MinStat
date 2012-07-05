using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Bonch.Security
{
  public class MinStatPrincipal : IPrincipal
  {
    public IIdentity Identity
    {
      get { throw new NotImplementedException(); }
    }

    public bool IsInRole(string role)
    {
      throw new NotImplementedException();
    }
  }
}
