// -----------------------------------------------------------------------
// <copyright file="ISecurityHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Bonch.Security.Abstract
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Bonch.Domain.POCO;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public interface ISecurityHelper
  {
    void SetAuthCookie(User user, bool rememberMe);
    MinStatPrincipal GetAuthCookie();
  }
}
