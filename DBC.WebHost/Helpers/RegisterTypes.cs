using DBC.Domain;
using DBC.Infrastructure.AutoMapperConfigs;
using DBC.Infrastructure.DataAccess;
using DBC.WcfServices;
using Microsoft.Extensions.DependencyInjection;

namespace DBC.WebHost.Helpers
{
    public class RegisterTypes
    {
        public static void Register(IServiceCollection container)
        {
            container.AddSingleton<IDatabaseBuildHelper, DatabaseBuildHelper>();
            container.AddDomain();
            container.AddWcfServices();

            // AutoMapper
            var mapper = AutoMapperConfig.InitializeAutoMapperForWcf().CreateMapper();
            container.AddSingleton(mapper);
        }
    }
}
