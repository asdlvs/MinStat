// -----------------------------------------------------------------------
// <copyright file="IValidate.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Bonch.Security.Abstract
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;


  public interface IValidate
  {
    bool Validate(string username, string password);
  }
}
