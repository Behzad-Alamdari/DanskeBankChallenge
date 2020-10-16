using DBC.Contracts.DataContracts;
using DBC.Contracts.ServiceContracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DBC.Proxies
{
    public class MunicipalityTaxClient : ClientBase<IMunicipalityTaxService>, IMunicipalityTaxService
    {
        public Task<List<MunicipalityVw>> AddMunicipalitiesAsync()
        {
            return Channel.AddMunicipalitiesAsync();
        }

        public Task<MunicipalityVw> AddMunicipalityAsync(string name)
        {
            return Channel.AddMunicipalityAsync(name);
        }

        public Task<string> AddTaxRuleAsync(string forMunicipality, TaxRuleDto rule)
        {
            return Channel.AddTaxRuleAsync(forMunicipality, rule);
        }

        public Task<bool> DoesMunicipalityExistAsync(string name)
        {
            return Channel.DoesMunicipalityExistAsync(name);
        }

        public Task<float> FindApplicableTaxAsync(string municipalityName, DateTime date)
        {
            return Channel.FindApplicableTaxAsync(municipalityName, date);
        }

        public Task<List<TaxRuleVw>> FindMunicipalityTaxRulesAsync(string municipalityName)
        {
            return Channel.FindMunicipalityTaxRulesAsync(municipalityName);
        }
    }
}
