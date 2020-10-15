using DBC.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DBC.Infrastructure.DataAccess.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        /// <summary>
        /// Asynchronously finds an entity with the given primary key values. If an entity with the given
        /// primary key values is being tracked by the context, then it is returned immediately
        /// without making a request to the database.Otherwise, a query is made to the database
        /// for an entity with the given primary key values and this entity, if found, is
        /// attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Entity</returns>
        Task<T> GetAsync(Guid id);

        /// <summary>
        /// Asynchronously finds an entity with the given conditions. A query is made to the database
        /// for an entity with the given conditions, if found, is
        /// attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="predicate">Conditions by which the entity will be selected</param>
        /// <returns>Entity</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously selects a list of entities from database as tracking.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A list of entities</returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate = null);


        /// <summary>
        /// Begins tracking the given entity, and any other reachable entities that are not
        /// already being tracked, in the Added state so that they will be inserted into
        /// the database when SaveChanges is called.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Entity id</returns>
        Guid Add(T entity);
    }
}
