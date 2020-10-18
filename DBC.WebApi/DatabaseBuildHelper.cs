using DBC.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DBC.WebApi
{
    // This is done to let test have different configuration and use SQLite instead of SqlServer
    public class DatabaseBuildHelper : IDatabaseBuildHelper
    {
        private readonly IConfiguration _configuration;

        public DatabaseBuildHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Build(DbContextOptionsBuilder builder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
        }
    }
}
