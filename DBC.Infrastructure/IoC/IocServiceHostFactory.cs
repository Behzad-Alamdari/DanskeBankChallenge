using Microsoft.Extensions.DependencyInjection;
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace DBC.Infrastructure.IoC
{
    public abstract class IocServiceHostFactory : ServiceHostFactory
    {
        protected abstract void ConfigureContainer(IServiceCollection container);

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var container = new ServiceCollection();

            ConfigureContainer(container);

            var provider = container.BuildServiceProvider();

            return new IocServiceHost(provider, serviceType, baseAddresses);
        }
    }
}
