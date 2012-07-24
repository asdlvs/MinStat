using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;
using System.Net;

namespace MinStat.Enterprises.WebUI.Interceptors
{
    public class CookieMessageInspector : IClientMessageInspector
    {
        public CookieMessageInspector()
        {

        }

        public void AfterReceiveReply(ref Message reply,
            object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request,
            System.ServiceModel.IClientChannel channel)
        {
            string cookieHolder = HttpContext.Current.Session["cookieHolder"] as string;
            if (string.IsNullOrEmpty(cookieHolder))
            {
              return null;
            }

            HttpRequestMessageProperty requestMessageProperty = new HttpRequestMessageProperty();
            requestMessageProperty.Headers[HttpResponseHeader.SetCookie] = cookieHolder;
            request.Properties[HttpRequestMessageProperty.Name] = requestMessageProperty;

            return null;
        }
    }
}