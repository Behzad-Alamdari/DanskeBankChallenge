using Microsoft.Extensions.DependencyInjection;

namespace DBC.WcfServiceLibrary
{
    public static class Module
    {
        public static void AddWcfServiceLibrary(this IServiceCollection services)
        {
            services.AddTransient<IMunicipalityTaxService, MunicipalityTaxService>();
            services.AddTransient<IMunicipalityTaxRuleService, MunicipalityTaxRuleService>();
        }
    }
}
