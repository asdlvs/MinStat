// -----------------------------------------------------------------------
// <copyright file="IUserNotificator.cs" company="Microsoft">
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
  public interface IUserNotificator
  {
    void SendMessage(User user, NotificationMessage message);
  }
}
