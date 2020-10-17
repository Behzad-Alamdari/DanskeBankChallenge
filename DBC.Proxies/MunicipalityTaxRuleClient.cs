using DBC.Contracts.DataContracts;
using DBC.Contracts.ServiceContracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DBC.Proxies
{
    public class MunicipalityTaxRuleClient : ClientBase<IMunicipalityTaxRuleService>, IMunicipalityTaxRuleService
    {

        public Task<TaxRuleVw> AddTaxRuleAsync(Guid municipalityId, TaxRuleDto rule)
        {
            return Channel.AddTaxRuleAsync(municipalityId, rule);
        }

        public Task<TaxRuleVw> EditTaxRuleAsync(Guid taxRuleId, TaxRuleDto rule)
        {
            return Channel.EditTaxRuleAsync(taxRuleId, rule);
        }

        public Task<List<PeriodVw>> GetPeriodsAsync(Guid taxRuleId)
        {
            return Channel.GetPeriodsAsync(taxRuleId);
        }
    }
}
