// -----------------------------------------------------------------------
// <copyright file="IEnterpriseRegistrator.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Bonch.Domain.Abstract
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
using Bonch.Domain.POCO;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public interface IEnterpriseRegistrator
  {
    IEnumerable<Enterprise> GetEnterprises();

    IEnumerable<User> GetUsers(Enterprise enterprise);

    void CreateUser(User user, Enterprise enterprise);

    void DeleteUser(User user, Enterprise enterprise);
  }
}
