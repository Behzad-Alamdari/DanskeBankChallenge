using DBC.Contracts.DataContracts;
using DBC.Contracts.ServiceContracts;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DBC.Proxies
{
    public class MunicipalityTaxRuleClient : ClientBase<IMunicipalityTaxRuleService>, IMunicipalityTaxRuleService
    {
        public Task<string> AddPeriod(Guid taxRuleId, PeriodDto period)
        {
            return Channel.AddPeriod(taxRuleId, period);
        }
    }
}
