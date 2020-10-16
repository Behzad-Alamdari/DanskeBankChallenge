using AutoMapper;
using DBC.Infrastructure.DataContracts;
using DBC.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DBC.WcfServiceLibrary
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class MunicipalityTaxService : IMunicipalityTaxService
    {
        private readonly IMunicipalityService _municipalityService;
        private readonly IMapper _mapper;

        public MunicipalityTaxService(IMunicipalityService municipalityService, IMapper mapper)
        {
            _municipalityService = municipalityService;
            _mapper = mapper;
        }

        public async Task<string> AddMunicipalityAsync(string name)
        {
            return await _municipalityService.AddAsync(name);
        }

        public async Task<string> AddTaxRuleAsync(string forMunicipality, TaxRuleDto rule)
        {
            var taxRule = _mapper.Map<Models.TaxRule>(rule);
            var message = await _municipalityService.AddTaxRule(forMunicipality, taxRule);
            return message;
        }

        [FaultContract(typeof(FaultHandle))]
        public async Task<float> FindApplicableTaxAsync(string municipalityName, DateTime date)
        {
            var (percentage, message) = await _municipalityService.FindApplicableTax(municipalityName, date);
            if (!string.IsNullOrEmpty(message))
            {
                var fault = new FaultHandle(message);
                throw new FaultException<FaultHandle>(fault);
            }    
                return percentage;
        }

        public async Task<List<TaxRuleVw>> FindMunicipalityTaxRulesAsync(string municipalityName)
        {
            var rules = await _municipalityService.FindMunicipalityTaxRules(municipalityName);
            return _mapper.Map<List<TaxRuleVw>>(rules);
        }
    }
}
