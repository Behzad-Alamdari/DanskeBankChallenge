using DBC.Infrastructure.DataAccess;
using System.Configuration;

namespace DBC.ConsoleHost
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
