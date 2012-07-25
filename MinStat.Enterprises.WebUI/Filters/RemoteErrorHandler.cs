using System;
using System.Web.Mvc;
using System.Web.Security;

namespace MinStat.Enterprises.WebUI.Filters
{
  using System.Collections.Generic;
  using System.ServiceModel;
  using System.Web;

  using MinStat.Enterprises.WebUI.Models;

  public class RemoteErrorHandlerAttribute : Attribute, IExceptionFilter
  {
    public void OnException(ExceptionContext filterContext)
    {
      if (filterContext.Exception.Message.Equals("wrong username", StringComparison.OrdinalIgnoreCase))
      {
        filterContext.ExceptionHandled = true;
        FormsAuthentication.SignOut();
        filterContext.Result = new RedirectResult("~/Account/Index");
      }
    }
  }
}