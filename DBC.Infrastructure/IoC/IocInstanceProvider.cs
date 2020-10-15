using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace DBC.Infrastructure.IoC
{
    public class IocInstanceProvider : IInstanceProvider
    {
        private readonly IServiceProvider _container;
        private readonly Type _contractType;

        public IocInstanceProvider(IServiceProvider container, Type contractType)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (contractType == null)
            {
                throw new ArgumentNullException("contractType");
            }

            _container = container;
            _contractType = contractType;
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            var childContainer =
              instanceContext.Extensions.Find<IocInstanceContextExtension>().GetChildContainer(_container);

            return childContainer.GetService(_contractType);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            instanceContext.Extensions.Find<IocInstanceContextExtension>().DisposeOfChildContainer();
        }
    }
}
