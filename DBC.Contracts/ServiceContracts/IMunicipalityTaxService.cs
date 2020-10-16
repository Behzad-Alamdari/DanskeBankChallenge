using DBC.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DBC.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IMunicipalityTaxService
    {
        [OperationContract]
        Task<MunicipalityVw> AddMunicipalityAsync(string name);


        [OperationContract]
        Task<bool> DoesMunicipalityExistAsync(string name);


        [OperationContract]
        Task<List<MunicipalityVw>> AddMunicipalitiesAsync();


        [OperationContract]
        Task<string> AddTaxRuleAsync(string forMunicipality, TaxRuleDto rule);


        [OperationContract]
        Task<float> FindApplicableTaxAsync(string municipalityName, DateTime date);


        [OperationContract]
        Task<List<TaxRuleVw>> FindMunicipalityTaxRulesAsync(string municipalityName);

    }
}
