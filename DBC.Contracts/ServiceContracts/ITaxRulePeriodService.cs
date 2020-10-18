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
        [FaultContract(typeof(FaultHandle))]
        Task<PeriodVw> AddTaxRulePeriodAsync(Guid taxRuleId, PeriodDto period);


        [OperationContract]
        [FaultContract(typeof(FaultHandle))]
        Task<PeriodVw> EditTaxRulePeriodAsync(Guid taxRulePeriodId, PeriodDto period);


        [OperationContract]
        [FaultContract(typeof(FaultHandle))]
        Task DeleteTaxRulePeriodAsync(Guid taxRulePeriodId);

    }
}
