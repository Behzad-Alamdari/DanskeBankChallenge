using DBC.DataAccess.EntityFramework;
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
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly DansBankDbContext Context;

        public Repository(DansBankDbContext context)
        {
            Context = context;
        }


        public Guid Add(T entity)
        {
            // If Id is empty, new id will assigned
            if (entity.Id.Equals(Guid.Empty))
                entity.Id = Guid.NewGuid();

            // Entity is added to context
            Context.Set<T>().Add(entity);

            // return the generated id
            return entity.Id;
        }

        public async Task<T> GetAsync(Guid id)
        {
            // Finds an entity with the given primary key values.
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            // Finds an entity with the given predicate.
            return await Context.Set<T>().Where(predicate)
                .FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate = null)
        {
            // Get items as IQueryable
            var query = Context.Set<T>()?.AsQueryable();

            // This should not happen, but to satisfy null reference checking, we do this check
            if (query == null)
                return new List<T>();

            // If predicate is not null, the condition will be added to query
            if (predicate != null)
                query = query.Where(predicate);

            // Return the item list
            return await query.ToListAsync();
        }
    }
}
