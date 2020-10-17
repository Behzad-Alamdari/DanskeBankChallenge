using DBC.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DBC.Contracts.ServiceContracts
{

    [ServiceContract]
    public interface IMunicipalityTaxRuleService
    {

        [OperationContract]
        Task<TaxRuleVw> AddTaxRuleAsync(Guid municipalityId, TaxRuleDto rule);


        [OperationContract]
        Task<TaxRuleVw> EditTaxRuleAsync(Guid taxRuleId, TaxRuleDto rule);


        [OperationContract]
        Task<List<PeriodVw>> GetPeriodsAsync(Guid taxRuleId);

    }
}
