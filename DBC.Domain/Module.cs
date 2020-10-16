using DBC.DataAccess;
using DBC.Domain.Services;
using DBC.Domain.Utilities;
using DBC.Infrastructure.Domains;
using DBC.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace DBC.Domain
{
    public static class Module
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddDataAccess();

            services.AddScoped<IMunicipalityDomainLogic, MunicipalityService>();
            services.AddScoped<ITaxRuleDomainService, TaxRuleDomainService>();
            services.AddTransient<ITaxCanculator, TaxCanculator>();
        }
    }
}
