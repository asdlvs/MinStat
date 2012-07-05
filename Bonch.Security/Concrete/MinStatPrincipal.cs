﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using Bonch.Domain.POCO;

namespace Bonch.Security
{
  public class MinStatPrincipal : IPrincipal
  {

    private IIdentity _identity;
    public MinStatPrincipal(User user)
    {
      _identity = new MinStatIdentity 
      { 
          Id = user.Id, 
          Mail = user.Mail, 
          EnterpriseId = user.EnterpriseId,
          FirstName = user.FirstName,
          LastName = user.LastName,
          Phone = user.Phone
      };
    }

    public IIdentity Identity
    {
      get
      {
        return _identity;
      }
    }

    public bool IsInRole(string role)
    {
      throw new NotImplementedException();
    }
  }
}
