using DBC.Contracts.DataContracts;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DBC.Contracts.ServiceContracts
{

    [ServiceContract]
    public interface IMunicipalityTaxRuleService
    {
        [OperationContract]
        Task<string> AddPeriod(Guid taxRuleId, PeriodDto period);
    }
}
