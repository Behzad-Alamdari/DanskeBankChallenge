using DBC.Infrastructure.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace DBC.WebHost.Helpers
{
    public class MunicipalityTaxServiceFactory : IocServiceHostFactory
    {
        protected override void ConfigureContainer(IServiceCollection container)
        {
            RegisterTypes.Register(container);
        }
    }
}
