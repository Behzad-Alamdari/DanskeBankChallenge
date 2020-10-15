using DBC.Models;
using System;
using System.Threading.Tasks;

namespace DBC.Infrastructure.DataAccess.Repositories
{
    public interface ITaxRuleRepository : IRepository<TaxRule>
    {
        Task<TaxRule> GetWithDetails(Guid id);
    }
}
