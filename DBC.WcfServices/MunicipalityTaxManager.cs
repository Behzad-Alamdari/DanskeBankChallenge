using AutoMapper;
using DBC.Contracts.DataContracts;
using DBC.Contracts.ServiceContracts;
using DBC.Infrastructure.Domains;
using DBC.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DBC.WcfServices
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.PerCall)]
    [FaultExceptionHandler]
    public class MunicipalityTaxManager : IMunicipalityTaxService, IMunicipalityTaxRuleService,
        ITaxRulePeriodService
    {
        private readonly IMunicipalityDomainLogic _municipalityService;
        private readonly ITaxRuleDomainService _taxRuleDomainService;
        private readonly ITaxRulePeriodDomainService _taxRulePeriodDomainService;
        private readonly IMapper _mapper;

        public MunicipalityTaxManager(IMunicipalityDomainLogic municipalityService,
            ITaxRuleDomainService taxRuleDomainService,
            ITaxRulePeriodDomainService taxRulePeriodDomainService, IMapper mapper)
        {
            _municipalityService = municipalityService;
            _taxRuleDomainService = taxRuleDomainService;
            _taxRulePeriodDomainService = taxRulePeriodDomainService;
            _mapper = mapper;
        }

        #region IMunicipalityTaxService

        public async Task<bool> DoesMunicipalityExistAsync(string name)
        {
            return await _municipalityService.Exist(name);
        }

        public async Task<MunicipalityVw> AddMunicipalityAsync(string name)
        {
            var (municipality, error) = await _municipalityService.AddAsync(name);
            if (!string.IsNullOrWhiteSpace(error))
            {
                var fault = new FaultHandle(error);
                throw new FaultException<FaultHandle>(fault);
            }

            return _mapper.Map<MunicipalityVw>(municipality);
        }

        public async Task<float> FindApplicableTaxAsync(Guid municipalityId, DateTime date)
        {
            var (percentage, message) = await _municipalityService.FindApplicableTax(municipalityId, date);
            if (!string.IsNullOrEmpty(message))
            {
                var fault = new FaultHandle(message);
                throw new FaultException<FaultHandle>(fault);
            }
            return percentage;
        }

        public async Task<List<TaxRuleVw>> FindMunicipalityTaxRulesAsync(Guid municipalityId)
        {
            var rules = await _municipalityService.FindMunicipalityTaxRules(municipalityId);
            return _mapper.Map<List<TaxRuleVw>>(rules);
        }

        public async Task<List<MunicipalityVw>> GetMunicipalitiesAsync()
        {
            var (municipalites, totalCount) = await _municipalityService.GetMunicipalities();
            return _mapper.Map<List<MunicipalityVw>>(municipalites);
        }

        public async Task<MunicipalityVw> EditMunicipalityAsync(Guid id, string newName)
        {
            var (municipality, error) = await _municipalityService.EditAsync(id, newName);
            if (!string.IsNullOrEmpty(error))
            {
                var fault = new FaultHandle(error);
                throw new FaultException<FaultHandle>(fault);
            }
            return _mapper.Map<MunicipalityVw>(municipality);
        }

        #endregion

        #region IMunicipalityTaxRuleService

        public async Task<TaxRuleVw> AddTaxRuleAsync(Guid municipalityId, TaxRuleDto rule)
        {
            var taxRule = _mapper.Map<TaxRule>(rule);
            await _taxRuleDomainService.AddTaxRuleAsync(municipalityId, taxRule);
            return _mapper.Map<TaxRuleVw>(taxRule);
        }

        public async Task<TaxRuleVw> EditTaxRuleAsync(Guid taxRuleId, TaxRuleDto rule)
        {
            var taxRule = _mapper.Map<TaxRule>(rule);
            var error = await _taxRuleDomainService.EditTaxRuleAsync(taxRuleId, taxRule);

            if (!string.IsNullOrWhiteSpace(error))
            {
                var fault = new FaultHandle(error);
                throw new FaultException<FaultHandle>(fault);
            }

            return _mapper.Map<TaxRuleVw>(taxRule);
        }

        public async Task<List<PeriodVw>> GetPeriodsAsync(Guid taxRuleId)
        {
            var periods = await _taxRuleDomainService.GetTaxRulePeriodsAsync(taxRuleId);
            return _mapper.Map<List<PeriodVw>>(periods);
        }

        #endregion

        #region ITaxRulePeriodService

        public async Task<PeriodVw> AddTaxRulePeriodAsync(Guid taxRuleId, PeriodDto period)
        {
            var p = _mapper.Map<Period>(period);
            var (addedPeriod, error) = await _taxRulePeriodDomainService.AddPeriod(taxRuleId, p);

            if (!string.IsNullOrWhiteSpace(error))
            {
                var fault = new FaultHandle(error);
                throw new FaultException<FaultHandle>(fault);
            }

            return _mapper.Map<PeriodVw>(addedPeriod);
        }

        public async Task<PeriodVw> EditTaxRulePeriodAsync(Guid taxRulePeriodId, PeriodDto period)
        {
            var p = _mapper.Map<Period>(period);
            var (editedPeriod, error) = await _taxRulePeriodDomainService.EditPeriod(taxRulePeriodId, p);

            if (!string.IsNullOrWhiteSpace(error))
            {
                var fault = new FaultHandle(error);
                throw new FaultException<FaultHandle>(fault);
            }

            return _mapper.Map<PeriodVw>(editedPeriod);
        }

        public async Task DeleteTaxRulePeriodAsync(Guid taxRulePeriodId)
        {
            var error = await _taxRulePeriodDomainService.DeletePeriod(taxRulePeriodId);


            if (!string.IsNullOrWhiteSpace(error))
            {
                var fault = new FaultHandle(error);
                throw new FaultException<FaultHandle>(fault);
            }
        }

        #endregion

    }
}
