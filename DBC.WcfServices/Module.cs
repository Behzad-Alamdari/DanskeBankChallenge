using DBC.Contracts.ServiceContracts;
using Microsoft.Extensions.DependencyInjection;

namespace DBC.WcfServices
{
    public static class Module
    {
        public static void AddWcfServices(this IServiceCollection services)
        {
            services.AddTransient<IMunicipalityTaxService, MunicipalityTaxService>();
            services.AddTransient<IMunicipalityTaxRuleService, MunicipalityTaxRuleService>();
        }
    }
}
