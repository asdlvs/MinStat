using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using MinStat.Enterprises.BLL;
using System.Net;

namespace MinStat.Enterprises.BLL.IISHosting
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            System.Web.ApplicationServices.AuthenticationService.Authenticating += AuthenticationService_Authenticating;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
           
        }

        void AuthenticationService_Authenticating(object sender, System.Web.ApplicationServices.AuthenticatingEventArgs e)
        {
            e.Authenticated = new UserValidator().Validate(e.UserName, e.Password);
            e.AuthenticationIsComplete = true;

            if (e.Authenticated)
            {
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName);
                cookie.Value = e.UserName;

                HttpResponseMessageProperty response = new HttpResponseMessageProperty();
                response.Headers[HttpResponseHeader.SetCookie] = cookie.Name + "=" + cookie.Value;
                OperationContext.Current.OutgoingMessageProperties
                     [HttpResponseMessageProperty.Name] = response;
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}