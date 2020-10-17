using DBC.Contracts.ServiceContracts;
using Microsoft.Extensions.DependencyInjection;

namespace DBC.WcfServices
{
    public static class Module
    {
        public static void AddWcfServices(this IServiceCollection services)
        {
            services.AddTransient<IMunicipalityTaxService, MunicipalityTaxManager>();
            services.AddTransient<IMunicipalityTaxRuleService, MunicipalityTaxManager>();
            services.AddTransient<ITaxRulePeriodService, MunicipalityTaxManager>();
        }
    }
}
