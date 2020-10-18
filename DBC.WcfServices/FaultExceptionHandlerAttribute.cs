using DBC.Contracts.DataContracts;
using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace DBC.WcfServices
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FaultExceptionHandlerAttribute : Attribute, IErrorHandler, IServiceBehavior
    {

        #region IErrorHandler
        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (!(error is FaultException<FaultHandle>))
            {
                var faultHandle = new FaultHandle($"Something went wrong in the server side.{Environment.NewLine}Please contact your administrator");
                FaultException<FaultHandle> faultException =
                    new FaultException<FaultHandle>(faultHandle);

                fault = Message.CreateMessage(version, faultException.CreateMessageFault(), faultException.Action);
            }
        }

        #endregion

        #region IServiceBehavior

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
                channelDispatcher.ErrorHandlers.Add(this);
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        #endregion
    }
}
