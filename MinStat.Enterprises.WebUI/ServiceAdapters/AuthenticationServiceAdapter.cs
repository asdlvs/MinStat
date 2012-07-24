using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using MinStat.Enterprises.WebUI.AuthenticationServiceReference;

namespace MinStat.Enterprises.WebUI.ServiceAdapters
{
    public class AuthenticationServiceAdapter : IAuthenticationServiceAdapter
    {
        public bool Login(string username, string password, string customCredential, bool isPersistent)
        {
            AuthenticationServiceClient proxy = new AuthenticationServiceClient();
            using (new OperationContextScope(proxy.InnerChannel))
            {
                bool result = proxy.Login(username, password, customCredential, isPersistent);
                var responseMessageProperty = (HttpResponseMessageProperty)
                     OperationContext.Current.IncomingMessageProperties[HttpResponseMessageProperty.Name];

                if (result)
                {
                    HttpContext.Current.Session["cookieHolder"] = responseMessageProperty.Headers.Get("Set-Cookie");
                }
                return result;
            }
        }

        public void Logout()
        {
            using (AuthenticationServiceClient proxy = new AuthenticationServiceClient())
            {
                proxy.Logout();
            }
        }

        public bool ValidateUser(string username, string password, string customCredential)
        {
            using (AuthenticationServiceClient proxy = new AuthenticationServiceClient())
            {
                return proxy.ValidateUser(username, password, customCredential);
            }
        }
    }
}