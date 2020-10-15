using DBC.Infrastructure.DataContracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DBC.WcfServiceLibrary
{
    [ServiceContract]
    public interface IMunicipalityTaxService
    {
        [OperationContract]
        Task<string> AddMunicipalityAsync(string name);

        [OperationContract]
        Task<string> AddTaxRuleAsync(string forMunicipality, TaxRuleDto rule);

        [OperationContract]
        Task<float> FindApplicableTaxAsync(string municipalityName, DateTime date);

        [OperationContract]
        Task<List<TaxRuleVw>> FindMunicipalityTaxRulesAsync(string municipalityName);
    }
}
