using DBC.Contracts.DataContracts;
using DBC.Contracts.ServiceContracts;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DBC.Proxies
{
    public class TaxRulePeriodClient : ClientBase<ITaxRulePeriodService>, ITaxRulePeriodService
    {
        public Task<PeriodVw> AddTaxRulePeriodAsync(Guid taxRuleId, PeriodDto period)
        {
            return Channel.AddTaxRulePeriodAsync(taxRuleId, period);
        }

        public Task DeleteTaxRulePeriodAsync(Guid taxRulePeriodId)
        {
            return Channel.DeleteTaxRulePeriodAsync(taxRulePeriodId);
        }

        public Task<PeriodVw> EditTaxRulePeriodAsync(Guid taxRulePeriodId, PeriodDto period)
        {
            return Channel.EditTaxRulePeriodAsync(taxRulePeriodId, period);
        }
    }
}
