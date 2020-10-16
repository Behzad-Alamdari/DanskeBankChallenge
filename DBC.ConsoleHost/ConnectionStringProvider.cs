using DBC.Infrastructure.DataAccess;
using Microsoft.Data.Sqlite;
using System.Configuration;
using System.Data.Common;

namespace DBC.ConsoleHost
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public DbConnection Connection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return new SqliteConnection(connectionString);
        }
    }
}
