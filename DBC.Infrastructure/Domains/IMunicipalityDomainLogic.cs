using DBC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBC.Infrastructure.Domains
{
    public interface IMunicipalityDomainLogic
    {
        Task<bool> Exist(string name);
        Task<(Municipality addedMunicipality, string error)> AddAsync(string name);
        Task<(Municipality editedMunicipality, string error)> EditAsync(Guid id, string newName);
        Task<(float percentage, string message)> FindApplicableTax(Guid municipalityId, DateTime date);
        Task<Municipality> FindAsync(string name);
        Task<List<Municipality>> GetMunicipalities();
        Task<List<TaxRule>> FindMunicipalityTaxRules(Guid municipalityId);
    }
}