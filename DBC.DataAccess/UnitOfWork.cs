using DBC.DataAccess.EntityFramework;
using DBC.Infrastructure.DataAccess;
using System.Linq;
using System.Threading.Tasks;

namespace DBC.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DansBankDbContext _context;

        public UnitOfWork(DansBankDbContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            try
            {
                var affectedEntities = _context.ChangeTracker.Entries().Where(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added ||
                e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {

                throw;
            }

        }
    }
}
