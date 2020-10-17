using DBC.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DBC.Infrastructure.DataAccess.Repositories
{
    public interface IMunicipalityRepository : IRepository<Municipality>
    {
        /// <summary>
        /// Check if the municipality exist in the database
        /// </summary>
        /// <param name="municipalityName">The name of municipality</param>
        /// <returns>true if the municipality exist and false if it does not</returns>
        Task<bool> Exist(string municipalityName);

        /// <summary>
        /// Select the municipality along with tax rules and their periods
        /// </summary>
        /// <param name="municipalityId">The municipality Id</param>
        /// <returns>Municipality with tax rule and their periods</returns>
        Task<Municipality> GetWithDetails(Guid municipalityId);

        /// <summary>
        /// Asynchronously selects a list of entities from database as tracking.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="pagination">would define the required paging</param>
        /// <returns>A list of entities</returns>
        Task<List<Municipality>> GetListAsync(Pagination pagination = null,
            Expression<Func<Municipality, bool>> predicate = null);
    }
}
