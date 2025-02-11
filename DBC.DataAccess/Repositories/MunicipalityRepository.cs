﻿using DBC.DataAccess.EntityFramework;
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
            municipalityName = municipalityName?.ToLower();
            return await Context.Municipalities
                .AnyAsync(m => m.Name.ToLower() == municipalityName);
        }

        public async Task<(List<Municipality> municipalities, int totalCount)> GetListAsync(Pagination pagination = null,
            Expression<Func<Municipality, bool>> predicate = null)
        {
            // Get Municipalities as IQueryable
            var query = Context.Municipalities?.OrderBy(m => m.Name).AsQueryable();

            // This should not happen, but to satisfy null reference checking, we do this check
            if (query == null)
                return (new List<Municipality>(), 0);

            // If predicate is not null, the condition will be added to query
            if (predicate != null)
                query = query.Where(predicate);


            var municipalites = new List<Municipality>();
            if (pagination == null)
            {
                municipalites = await query.ToListAsync();
                return (municipalites, municipalites.Count);
            }

            // If pagination is not null, they will be applied to query
            var totalCount = await query.CountAsync();
            var municipalities = await query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize).ToListAsync();

            // Return the municipalities list
            return (municipalities, totalCount);
        }

        public async Task<Municipality> GetWithDetails(Guid municipalityId)
        {
            return await Context.Municipalities
                .Include(m => m.TaxRules).ThenInclude(t => t.Periods)
                .FirstOrDefaultAsync(m => m.Id == municipalityId);
        }
    }
}
