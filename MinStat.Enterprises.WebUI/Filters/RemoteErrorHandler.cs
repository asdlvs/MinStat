﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MinStat.Enterprises.WebUI.Filters
{
    public class RemoteErrorHandler :Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            FormsAuthentication.SignOut();
        }
    }
}