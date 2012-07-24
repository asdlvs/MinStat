using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace MinStat.Enterprises.BLL.Interceptors
{
    public class IdentityMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request,
               IClientChannel channel,
               InstanceContext instanceContext)
        {
            // Extract Cookie (name=value) from messageproperty
            var messageProperty = (HttpRequestMessageProperty)
                OperationContext.Current.IncomingMessageProperties[HttpRequestMessageProperty.Name];
            string cookie = messageProperty.Headers.Get("Set-Cookie");
            if (string.IsNullOrWhiteSpace(cookie))
                return null;

            string[] nameValue = cookie.Split('=', ',');
            string userName = string.Empty;

            // Set User Name from cookie
            int pos = nameValue.ToList().IndexOf(".ASPXAUTH");
            if (pos == -1)
                return null;

            userName = nameValue[pos + 1];

            // Set Thread Principal to User Name
            EnterpriseIdentity enterpriseIdentity = new EnterpriseIdentity();
            GenericPrincipal threadCurrentPrincipal =
                   new GenericPrincipal(enterpriseIdentity, new string[] { });
            enterpriseIdentity.IsAuthenticated = true;
            enterpriseIdentity.Name = userName;
            System.Threading.Thread.CurrentPrincipal = threadCurrentPrincipal;

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {

        }
    }
}
