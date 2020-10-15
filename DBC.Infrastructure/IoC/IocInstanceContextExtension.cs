using Microsoft.Extensions.DependencyInjection;
using System;
using System.ServiceModel;

namespace DBC.Infrastructure.IoC
{
    public class IocInstanceContextExtension : IExtension<InstanceContext>
    {
        private IServiceProvider _childContainer;
        private IServiceScope _serviceScope;

        public void Attach(InstanceContext owner)
        {
        }

        public void Detach(InstanceContext owner)
        {
        }

        public void DisposeOfChildContainer() => _serviceScope.Dispose();

        public IServiceProvider GetChildContainer(IServiceProvider container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (_serviceScope == null)
                _serviceScope = container.CreateScope();

            return _childContainer ?? (_childContainer = _serviceScope.ServiceProvider);
        }
    }
}
