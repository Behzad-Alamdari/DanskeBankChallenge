using DBC.DataAccess.EntityFramework;
using DBC.Domain;
using DBC.Infrastructure.AutoMapperConfigs;
using DBC.Infrastructure.DataAccess;
using DBC.WcfServices;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DBC.ConsoleHost
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
                    Console.WriteLine("Database is been created, please be patient. You will be notify when it is done");
                    db.Database.EnsureCreated();
                    Console.WriteLine("Database is been created");
                }

            }
        }
    }
}
