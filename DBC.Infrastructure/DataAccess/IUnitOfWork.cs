using System.Threading.Tasks;

namespace DBC.Infrastructure.DataAccess
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
