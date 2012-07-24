// -----------------------------------------------------------------------
// <copyright file="IdentityInspector.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MinStat.Enterprises.BLL
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Security.Principal;
  using System.ServiceModel;
  using System.ServiceModel.Channels;
  using System.Text;
  using System.ServiceModel.Dispatcher;
  using System.Web;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public class IdentityInspector : IClientMessageInspector, IDispatchMessageInspector
  {
    public void AfterReceiveReply(ref Message reply, object correlationState)
    {
      throw new NotImplementedException();
    }

    public object BeforeSendRequest(ref Message request, IClientChannel channel)
    {
      HttpRequestMessageProperty property = new HttpRequestMessageProperty();
      property.Headers.Add("username", HttpContext.Current.User.Identity.Name);
      request.Properties.Add("username", property);
      return null;
    }

    public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
    {
      string username = ((HttpRequestMessageProperty)request.Properties["username"]).Headers["username"];
      GenericIdentity identity = new GenericIdentity(username);
      HttpContext.Current.User = new GenericPrincipal(identity, null);
      return null;
    }

    public void BeforeSendReply(ref Message reply, object correlationState)
    {
      throw new NotImplementedException();
    }
  }
}
