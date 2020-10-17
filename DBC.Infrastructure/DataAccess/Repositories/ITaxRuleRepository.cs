using DBC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBC.Infrastructure.DataAccess.Repositories
{
    public interface ITaxRuleRepository : IRepository<TaxRule>
    {
        Task<TaxRule> GetWithDetailsAsync(Guid id);
        Task<List<Period>> GetPeriodsAsync(Guid id);
    }
}
