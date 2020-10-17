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
        public Task<List<MunicipalityVw>> GetMunicipalitiesAsync()
        {
            return Channel.GetMunicipalitiesAsync();
        }

        public Task<MunicipalityVw> AddMunicipalityAsync(string name)
        {
            return Channel.AddMunicipalityAsync(name);
        }

        public Task<bool> DoesMunicipalityExistAsync(string name)
        {
            return Channel.DoesMunicipalityExistAsync(name);
        }

        public Task<MunicipalityVw> EditMunicipalityAsync(Guid id, string newName)
        {
            return Channel.EditMunicipalityAsync(id, newName);
        }

        public Task<float> FindApplicableTaxAsync(Guid municipalityId, DateTime date)
        {
            return Channel.FindApplicableTaxAsync(municipalityId, date);
        }

        public Task<List<TaxRuleVw>> FindMunicipalityTaxRulesAsync(Guid municipalityId)
        {
            return Channel.FindMunicipalityTaxRulesAsync(municipalityId);
        }
    }
}
