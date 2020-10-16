using Microsoft.EntityFrameworkCore;

namespace DBC.Infrastructure.DataAccess
{
    public interface IDatabaseBuildHelper
    {
        void Build(DbContextOptionsBuilder builder);
    }
}
