using DBC.Infrastructure.DataContracts;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DBC.WcfServiceLibrary
{
    
    [ServiceContract]
    public interface IMunicipalityTaxRuleService
    {
        [OperationContract]
        Task<string> AddPeriod(Guid taxRuleId, PeriodDto period);
    }
}
