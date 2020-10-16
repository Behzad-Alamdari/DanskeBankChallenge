using DBC.Infrastructure.DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Test.DBC.DataAccess
{
    public class FackDataBaseBuildHelper : IDatabaseBuildHelper
    {
        SqliteConnection _connection;

        public FackDataBaseBuildHelper()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
        }

        public void Build(DbContextOptionsBuilder builder)
        {
            

            builder.UseSqlite(_connection);
        }
    }
}
