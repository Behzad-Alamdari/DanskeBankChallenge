using DBC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBC.Infrastructure.Services
{
    public interface IMunicipalityService
    {
        /// <summary>
        /// Add municipality to database
        /// </summary>
        /// <param name="name">The name of municipality to add</param>
        /// <returns>The newly added municipality ID</returns>
        Task<string> AddAsync(string name);

        /// <summary>
        /// Find a municipality from database
        /// </summary>
        /// <param name="name">The name of municipality to be found</param>
        /// <returns>The municipality</returns>
        Task<Municipality> FindAsync(string name);

        /// <summary>
        /// Add a tax rule to the given municipality
        /// </summary>
        /// <param name="municipalityName">The municipality name</param>
        /// <param name="taxRule">The tax rule to be added</param>
        /// <returns>Error text if necessary otherwise empty</returns>
        Task<string> AddTaxRule(string municipalityName, TaxRule taxRule);

        /// <summary>
        /// Find the tax which is applicable in a specific date
        /// </summary>
        /// <param name="municipalityName">The municipality name</param>
        /// <param name="date">The date of interest</param>
        /// <returns>Tax percentage applicable in the given date</returns>
        Task<float> FindApplicableTax(string municipalityName, DateTime date);


        /// <summary>
        /// Find all the rules registered for a municipality
        /// </summary>
        /// <param name="municipalityName">The municipality name</param>
        /// <returns>Registered tax rules of municipality</returns>
        Task<List<TaxRule>> FindMunicipalityTaxRules(string municipalityName);
    }
}
