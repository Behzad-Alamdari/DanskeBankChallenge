using DBC.Contracts.DataContracts;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DBC.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface ITaxRulePeriodService
    {

        [OperationContract]
        Task<PeriodVw> AddTaxRulePeriodAsync(Guid taxRuleId, PeriodDto period);


        [OperationContract]
        Task<PeriodVw> EditTaxRulePeriodAsync(Guid taxRulePeriodId, PeriodDto period);


        [OperationContract]
        Task DeleteTaxRulePeriodAsync(Guid taxRulePeriodId);

    }
}
