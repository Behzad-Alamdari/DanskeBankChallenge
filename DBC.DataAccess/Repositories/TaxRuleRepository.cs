using DBC.DataAccess.EntityFramework;
using DBC.Infrastructure.DataAccess.Repositories;
using DBC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DBC.DataAccess.Repositories
{
    public class TaxRuleRepository : Repository<TaxRule>, ITaxRuleRepository
    {
        public TaxRuleRepository(DansBankDbContext context)
            : base(context)
        {
        }

        public async Task<TaxRule> GetWithDetails(Guid id)
        {
            return await Context.TaxRules.Include(t => t.Periods)                
                 .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
