using DBC.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DBC.DataAccess.EntityFramework
{
    public class DatabaseGenerator : IHostedService
    {
        private readonly IServiceProvider _provider;

        public DatabaseGenerator(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _provider.CreateScope())
            {
                using (var db = (DansBankDbContext)scope.ServiceProvider.GetService(typeof(DansBankDbContext)))
                {
                    if (!(await db.Database.CanConnectAsync()))
                    {
                        await db.Database.EnsureCreatedAsync();

                        db.Municipalities.AddRange(SeedDataGenerator.Generate());
                        db.SaveChanges();
                    }

                }
            }
        }



        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
