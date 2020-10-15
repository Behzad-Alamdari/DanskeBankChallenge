using DBC.DataAccess.EntityFramework;
using DBC.Domain;
using DBC.Infrastructure.AutoMapperConfigs;
using DBC.Infrastructure.DataAccess;
using DBC.WcfServiceLibrary;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DBC.ConsoleHost
{
    public class Startup
    {
        public static IServiceProvider GetProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();
            services.AddDomain();
            services.AddWcfServiceLibrary();

            // AutoMapper
            var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            services.AddSingleton(mapper);

            var provider = services.BuildServiceProvider();

            using (var db = (DansBankDbContext)provider.GetService(typeof(DansBankDbContext)))
            {
                db.Database.EnsureCreated();
            }

            return provider;
        }
    }
}
