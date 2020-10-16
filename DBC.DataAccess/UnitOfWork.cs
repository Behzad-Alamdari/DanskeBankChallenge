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
            await _context.SaveChangesAsync();
        }
    }
}
