using DBC.DataAccess.EntityFramework;
using DBC.Infrastructure.DataAccess;
using DBC.Infrastructure.DataAccess.Repositories;
using DBC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DBC.DataAccess.Repositories
{
    public class MunicipalityRepository : Repository<Municipality>, IMunicipalityRepository
    {
        public MunicipalityRepository(DansBankDbContext context)
            : base(context)
        {
        }

        public async Task<bool> Exist(string municipalityName)
        {
            return await Context.Municipalities
                .AnyAsync(m => m.Name == municipalityName);
        }

        public async Task<List<Municipality>> GetListAsync(Pagination pagination, 
            Expression<Func<Municipality, bool>> predicate = null)
        {
            // Get Municipalities as IQueryable
            var query = Context.Municipalities?.OrderBy(m => m.Name).AsQueryable();
            
            // This should not happen, but to satisfy null reference checking, we do this check
            if (query == null)
                return new List<Municipality>();

            // If predicate is not null, the condition will be added to query
            if (predicate != null)
                query = query.Where(predicate);


            // If pagination is not null, they will be applied to query
                query = query
                    .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                    .Take(pagination.PageSize);

            // Return the municipalities list
            return await query.ToListAsync();
        }

        public async Task<Municipality> GetWithDetails(string municipalityName)
        {
            return await Context.Municipalities
                .Include(m => m.TaxRules).ThenInclude(t => t.Periods)
                .FirstOrDefaultAsync();
        }
    }
}
