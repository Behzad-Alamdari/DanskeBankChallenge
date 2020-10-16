using AutoMapper;
using DBC.Contracts.DataContracts;
using DBC.Contracts.ServiceContracts;
using DBC.Infrastructure.Domains;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DBC.WcfServices
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class MunicipalityTaxService : IMunicipalityTaxService, IMunicipalityTaxRuleService
    {
        private readonly IMunicipalityDomainLogic _municipalityService;
        private readonly ITaxRuleDomainService _taxRuleDomainService;
        private readonly IMapper _mapper;

        public MunicipalityTaxService(IMunicipalityDomainLogic municipalityService,
            ITaxRuleDomainService taxRuleDomainService, IMapper mapper)
        {
            _municipalityService = municipalityService;
            _taxRuleDomainService = taxRuleDomainService;
            _mapper = mapper;
        }

        public async Task<MunicipalityVw> AddMunicipalityAsync(string name)
        {
            var (municipality, error) = await _municipalityService.AddAsync(name);
            if(!string.IsNullOrWhiteSpace(error))
            {
                var fault = new FaultHandle(error);
                throw new FaultException<FaultHandle>(fault);
            }

            return _mapper.Map<MunicipalityVw>(municipality);
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

        public async Task<string> AddPeriod(Guid taxRuleId, PeriodDto period)
        {
            var p = _mapper.Map<Models.Period>(period);
            return await _taxRuleDomainService.AddPeriod(taxRuleId, p);
        }

        public async Task<List<MunicipalityVw>> AddMunicipalitiesAsync()
        {
            var municipalites = await _municipalityService.GetMunicipalities();
            return _mapper.Map<List<MunicipalityVw>>(municipalites);
        }

        public async Task<bool> DoesMunicipalityExistAsync(string name)
        {
            return await _municipalityService.Exist(name);
        }
    }
}
