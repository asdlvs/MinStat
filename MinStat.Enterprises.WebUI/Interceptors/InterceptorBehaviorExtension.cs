using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Web;

namespace MinStat.Enterprises.WebUI.Interceptors
{
    public class InterceptorBehaviorExtension : BehaviorExtensionElement, IEndpointBehavior
    {
        public override System.Type BehaviorType
        {
            get { return typeof(InterceptorBehaviorExtension); }
        }

        protected override object CreateBehavior()
        {
            return new InterceptorBehaviorExtension();
        }

        public void AddBindingParameters(ServiceEndpoint endpoint,
               System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint,
               System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new CookieMessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint,
               System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {

        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }
    }
}