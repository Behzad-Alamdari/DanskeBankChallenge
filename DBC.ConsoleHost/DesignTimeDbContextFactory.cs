using DBC.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace DBC.ConsoleHost
{
    // It is used by Entity Framework tools to generate migration
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DansBankDbContext>
    {
        public DansBankDbContext CreateDbContext(string[] args)
        {
            var provider = Startup.GetProvider();
            return provider.GetRequiredService<DansBankDbContext>();
        }
    }
}
