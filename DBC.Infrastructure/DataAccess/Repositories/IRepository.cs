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
        /// Check the existence of the entity in the database
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>true if the entity exist, false if the entity does not exist</returns>
        Task<bool> Exist(Guid id);

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
        /// Asynchronously finds an entity with the given primary key values. If an entity with the given
        /// primary key values is being tracked by the context, then it is returned immediately
        /// without making a request to the database.Otherwise, a query is made to the database
        /// for an entity with the given primary key values and this entity, if found, is
        /// attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Entity</returns>
        Task<T> GetAsNoTrackingAsync(Guid id);

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

        /// <summary>
        ///  Begins tracking the given entity and entries reachable from the given entity
        ///  using the Microsoft.EntityFrameworkCore.EntityState.Modified state by default,
        ///  but see below for cases when a different state will be used.
        ///  Generally, no database interaction will be performed until Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        ///  is called.
        ///  A recursive search of the navigation properties will be performed to find reachable
        ///  entities that are not already being tracked by the context. All entities found
        ///  will be tracked by the context.
        ///  For entity types with generated keys if an entity has its primary key value set
        ///  then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Modified
        ///  state. If the primary key value is not set then it will be tracked in the Microsoft.EntityFrameworkCore.EntityState.Added
        ///  state. This helps ensure new entities will be inserted, while existing entities
        ///  will be updated. An entity is considered to have its primary key value set if
        ///  the primary key property is set to anything other than the CLR default for the
        ///  property type.
        ///  For entity types without generated keys, the state set is always Microsoft.EntityFrameworkCore.EntityState.Modified.
        ///  Use Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry.State to set the
        ///  state of only a single entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Entity id</returns>
        void Edit(T entity);

        /// <summary>
        /// Mark the entity as deleted
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);
    }
}
