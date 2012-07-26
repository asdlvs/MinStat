using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using MinStat.DAL.POCO;

namespace MinStat.DAL
{
    public class UserNotifier : IUserNotifier
    {
        private DatabaseContext _context;

        public UserNotifier()
        {
            _context = new DatabaseContext();
        }


        public void Notify(string mail, int enterpriseId)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(mail);
            message.Subject = "Вас добавили в статистический реестр министерства";
            message.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["smtplogin"]);
            string pwd = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 7);
            message.Body = String.Format("Логин: {0}    Пароль: {1}", mail, pwd);
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["smtpserver"]);
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtplogin"], ConfigurationManager.AppSettings["smtppwd"]);
            smtp.Send(message);
            CreateUser(mail, pwd, enterpriseId);
        }

        public void CreateUser(string mail, string pwd, int enterpriseId)
        {
            _context.Users.Add(new User{EnterpriseId = enterpriseId, Mail = mail, Password = pwd});
            _context.SaveChanges();
        }
    }
}
