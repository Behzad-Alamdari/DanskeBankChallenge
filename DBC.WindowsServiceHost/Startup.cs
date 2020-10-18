using DBC.DataAccess.EntityFramework;
using DBC.Domain;
using DBC.Infrastructure.AutoMapperConfigs;
using DBC.Infrastructure.DataAccess;
using DBC.Models;
using DBC.WcfServices;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DBC.WindowsServiceHost
{
    public class Startup
    {
        public static IServiceProvider GetProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IDatabaseBuildHelper, DatabaseBuildHelper>();
            services.AddDomain();
            services.AddWcfServices();

            // AutoMapper
            var mapper = AutoMapperConfig.InitializeAutoMapperForWcf().CreateMapper();
            services.AddSingleton(mapper);

            return services.BuildServiceProvider();
        }

        public static void EnsureDatabaseExistance(IServiceProvider provider)
        {
            using (var db = (DansBankDbContext)provider.GetService(typeof(DansBankDbContext)))
            {
                if (!db.Database.CanConnect())
                {                   
                    db.Database.EnsureCreated();

                    db.Municipalities.AddRange(SeedDataGenerator.Generate());
                    db.SaveChanges();
                }

            }
        }
    }
}
