// -----------------------------------------------------------------------
// <copyright file="NotificationMessage.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Bonch.Domain.POCO
{
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.Linq;
  using System.Text;

  using Bonch.Domain.Abstract;
  using System.Net.Mail;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public class NotificationMessage
  {

    public string Subject { get; set; }

    public string Content { get; set; }

  }
}
