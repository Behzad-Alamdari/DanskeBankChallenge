using Microsoft.Extensions.DependencyInjection;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace DBC.Infrastructure.IoC
{
    public class IocServiceHost : ServiceHost
    {
        public IocServiceHost(IServiceProvider container, Type serviceType, params Uri[] baseAddresses)
          : base(serviceType, baseAddresses)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            ApplyServiceBehaviors(container);

            ApplyContractBehaviors(container);

            foreach (var contractDescription in ImplementedContracts.Values)
            {
                var contractBehavior =
                  new IocContractBehavior(new IocInstanceProvider(container, contractDescription.ContractType));

                contractDescription.Behaviors.Add(contractBehavior);
            }
        }

        private void ApplyContractBehaviors(IServiceProvider container)
        {
            var registeredContractBehaviors = container.GetServices<IContractBehavior>();

            foreach (var contractBehavior in registeredContractBehaviors)
            {
                foreach (var contractDescription in ImplementedContracts.Values)
                {
                    contractDescription.Behaviors.Add(contractBehavior);
                }
            }
        }

        private void ApplyServiceBehaviors(IServiceProvider container)
        {
            var registeredServiceBehaviors = container.GetServices<IServiceBehavior>();

            foreach (var serviceBehavior in registeredServiceBehaviors)
            {
                Description.Behaviors.Add(serviceBehavior);
            }
        }
    }
}
