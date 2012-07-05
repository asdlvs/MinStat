using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.POCO;
using Bonch.Security.Concrete;

namespace Bonch.Security.Abstract
{
    public interface ISecurityHelper
    {
        void SetAuthCookie(User user, bool rememberMe);
        // MinStatPrincipal GetAuthCookie();
    }
}
