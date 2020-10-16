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
        Task<string> AddTaxRule(string municipalityName, TaxRule taxRule);
        Task<(float percentage, string message)> FindApplicableTax(string municipalityName, DateTime date);
        Task<Municipality> FindAsync(string name);
        Task<List<Municipality>> GetMunicipalities();
        Task<List<TaxRule>> FindMunicipalityTaxRules(string municipalityName);
    }
}