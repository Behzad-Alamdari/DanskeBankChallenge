using DBC.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DBC.WebHost.Helpers
{
    // This is done to let test have different configuration and use SQLite instead of SqlServer
    public class DatabaseBuildHelper : IDatabaseBuildHelper
    {
        public void Build(DbContextOptionsBuilder builder)
        {
            var connectionString = ConfigurationManager
                .ConnectionStrings["DefaultConnection"].ConnectionString;

            builder.UseSqlServer(connectionString);
        }
    }
}
