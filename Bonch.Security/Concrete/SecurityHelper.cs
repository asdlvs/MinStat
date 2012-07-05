// -----------------------------------------------------------------------
// <copyright file="SecurityHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Bonch.Security
{
  using System;
  using System.Collections.Generic;
  using System.Web.Security;
  using System.Xml.Serialization;

  using Bonch.Domain.POCO;
  using System.IO;
  using System.Web;
  using Bonch.Security.Abstract;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public class SecurityHelper : ISecurityHelper, IValidate, IUserRepository
  {
    public void SetAuthCookie(User user, bool rememberMe = false)
    {
      string serializedObject = null;
      using (StringWriter sw = new StringWriter())
      {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(User));
        xmlSerializer.Serialize(sw, user);
        serializedObject = sw.ToString();

      }
      FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
        1, 
        user.Mail, 
        DateTime.Now, 
        DateTime.Now.AddDays(30),
        rememberMe, 
        serializedObject);
      string encTicket = FormsAuthentication.Encrypt(ticket);
      HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
      HttpContext.Current.Response.Cookies.Add(cookie);
    }

    public MinStatPrincipal GetAuthCookie()
    {
      HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
      if(authCookie != null)
      {
        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(User));
        using (StringReader reader = new StringReader(ticket.UserData))
        {
          User user = (User)xmlSerializer.Deserialize(reader);
          MinStatPrincipal principal = new MinStatPrincipal(user);
        }
      }
      return null;
    }

    public bool Validate(string username, string password)
    {
      return username == "admin" && password == "admin";
    }

    public User GetUser(string username)
    {
      return new User { Id = 1, Mail = "admin@admin.ru", Enterprise = new Enterprise{Id = 1, FederationSubject = null, Title = "Агропром"}};
    }

    public void SetUser(User user)
    {
      if(true/*Пользователь существует*/)
      {

      }
      else
      {
        
      }
    }
  }
}
