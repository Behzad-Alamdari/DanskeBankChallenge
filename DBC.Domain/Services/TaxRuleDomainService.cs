using DBC.Infrastructure.DataAccess;
using DBC.Infrastructure.DataAccess.Repositories;
using DBC.Infrastructure.Domains;
using DBC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBC.Domain.Services
{
    public class TaxRuleDomainService : ITaxRuleDomainService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITaxRuleRepository _taxRuleRepository;

        public TaxRuleDomainService(IUnitOfWork unitOfWork, ITaxRuleRepository taxRuleRepository)
        {
            _unitOfWork = unitOfWork;
            _taxRuleRepository = taxRuleRepository;
        }


        public async Task<string> AddPeriod(Guid taxRuleId, Period period)
        {
            var taxRule = await _taxRuleRepository.GetWithDetailsAsync(taxRuleId);
            if (taxRule == null)
                return "The rule has not been found";

            taxRule.Periods.Add(period);

            await _unitOfWork.CommitAsync();

            return "The rule is added successfully";
        }

        public async Task<Guid> AddTaxRuleAsync(Guid municipalityId, TaxRule rule)
        {
            rule.MunicipalityId = municipalityId;
            var id = _taxRuleRepository.Add(rule);

            await _unitOfWork.CommitAsync();

            return id;
        }

        public async Task<string> EditTaxRuleAsync(Guid taxRuleId, TaxRule rule)
        {
            var databaseRule = await _taxRuleRepository.GetAsNoTrackingAsync(taxRuleId);
            if (databaseRule == null)
                return Messages.TaxRuleDoesNotExist;

            rule.Id = taxRuleId;
            rule.MunicipalityId = databaseRule.MunicipalityId;

            _taxRuleRepository.Edit(rule);

            await _unitOfWork.CommitAsync();

            return null;
        }

        public async Task<List<Period>> GetTaxRulePeriodsAsync(Guid taxRuleId)
        {
            return await _taxRuleRepository.GetPeriodsAsync(taxRuleId);
        }
    }
}
