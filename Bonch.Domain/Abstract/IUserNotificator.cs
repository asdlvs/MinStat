using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.POCO;
namespace Bonch.Domain.Abstract
{
    public interface IUserNotificator
    {
        void SendMessage(User user, NotificationMessage message);
    }
}
