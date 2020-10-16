using DBC.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace DBC.WebHost.Helpers
{
    // It is used by Entity Framework tools to generate migration
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DansBankDbContext>
    {

        public DansBankDbContext CreateDbContext(string[] args)
        {
            var container = new ServiceCollection();
            RegisterTypes.Register(container);
            var provider = container.BuildServiceProvider();

            return provider.GetRequiredService<DansBankDbContext>();
        }
    }
}
