using AutoMapper;
using DBC.Infrastructure.DataContracts;
using DBC.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace DBC.WcfServiceLibrary
{
    public class MunicipalityTaxRuleService : IMunicipalityTaxRuleService
    {
        private readonly ITaxRuleDomainService _taxRuleDomainService;
        private readonly IMapper _mapper;

        public MunicipalityTaxRuleService(ITaxRuleDomainService taxRuleDomainService, IMapper mapper)
        {
            _taxRuleDomainService = taxRuleDomainService;
            _mapper = mapper;
        }

        public async Task<string> AddPeriod(Guid taxRuleId, PeriodDto period)
        {
            var p = _mapper.Map<Models.Period>(period);
            return await _taxRuleDomainService.AddPeriod(taxRuleId, p);
        }
    }
}
