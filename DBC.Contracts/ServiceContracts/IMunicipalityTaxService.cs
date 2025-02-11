﻿using DBC.Contracts.DataContracts;
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
        [FaultContract(typeof(FaultHandle))]
        Task<MunicipalityVw> EditMunicipalityAsync(Guid id, string newName);


        [OperationContract]
        [FaultContract(typeof(FaultHandle))]
        Task<MunicipalityVw> AddMunicipalityAsync(string name);


        [OperationContract]
        Task<bool> DoesMunicipalityExistAsync(string name);


        [OperationContract]
        Task<List<MunicipalityVw>> GetMunicipalitiesAsync();


        [OperationContract]
        [FaultContract(typeof(FaultHandle))]
        Task<float> FindApplicableTaxAsync(Guid municipalityId, DateTime date);


        [OperationContract]
        Task<List<TaxRuleVw>> FindMunicipalityTaxRulesAsync(Guid municipalityId);

    }
}
