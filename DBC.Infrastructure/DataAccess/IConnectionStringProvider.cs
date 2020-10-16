using System.Data.Common;

namespace DBC.Infrastructure.DataAccess
{
    public interface IConnectionStringProvider
    {
        DbConnection Connection();
    }
}
