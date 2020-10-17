using DBC.DataAccess.EntityFramework;
using DBC.Infrastructure.DataAccess.Repositories;
using DBC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBC.DataAccess.Repositories
{
    public class TaxRuleRepository : Repository<TaxRule>, ITaxRuleRepository
    {
        public TaxRuleRepository(DansBankDbContext context)
            : base(context)
        {
        }

        public async Task<List<Period>> GetPeriodsAsync(Guid id)
        {
            return await Context.Periods.Where(p => p.TaxRuleId == id)
                .ToListAsync();
        }

        public async Task<TaxRule> GetWithDetailsAsync(Guid id)
        {
            return await Context.TaxRules.Include(t => t.Periods)                
                 .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
