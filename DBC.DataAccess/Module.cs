using DBC.DataAccess.EntityFramework;
using DBC.DataAccess.Repositories;
using DBC.Infrastructure.DataAccess;
using DBC.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DBC.DataAccess
{
    public static class Module
    {
        public static void AddDataAccess(this IServiceCollection services)
        {
            services.AddDbContext<DansBankDbContext>();

            services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
            services.AddScoped<ITaxRuleRepository, TaxRuleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
