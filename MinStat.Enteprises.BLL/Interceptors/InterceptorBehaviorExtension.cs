using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace MinStat.Enterprises.BLL.Interceptors
{
    public class InterceptorBehaviorAttribute : Attribute, IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription,
               System.ServiceModel.ServiceHostBase serviceHostBase,
               System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints,
               System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceDescription
               serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (var endpoint in dispatcher.Endpoints)
                {
                    endpoint.DispatchRuntime.MessageInspectors.Add(new IdentityMessageInspector());
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription,
               System.ServiceModel.ServiceHostBase serviceHostBase)
        {

        }
    }
}
