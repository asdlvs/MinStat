
namespace Bonch.Domain.POCO
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;

    using Bonch.Domain.Abstract;
    using System.Net.Mail;


    public class NotificationMessage
    {

        public string Subject { get; set; }

        public string Content { get; set; }

    }
}
