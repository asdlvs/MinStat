// -----------------------------------------------------------------------
// <copyright file="UserNotificator.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Bonch.Domain.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Net;
  using System.Text;
  using Bonch.Domain.Abstract;
  using Bonch.Domain.POCO;
  using System.Configuration;
  using System.Net.Mail;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public class UserNotificator : IUserNotificator
  {
    public void SendMessage(User user, NotificationMessage message)
    {
      string fromMail = ConfigurationManager.AppSettings["fromMail"];
      string toMail = user.Mail;
      string subject = message.Subject;
      string content = message.Content;
      MailMessage mailMessage = new MailMessage(fromMail, toMail, subject, content);
      string userName = ConfigurationManager.AppSettings["userName"];
      string password = ConfigurationManager.AppSettings["password"];
      string smtpClientHost = ConfigurationManager.AppSettings["smtpClientHost"];
      SmtpClient client = new SmtpClient(smtpClientHost);
      client.Credentials = new NetworkCredential(userName, password);
      client.Send(mailMessage);
    }
  }
}
