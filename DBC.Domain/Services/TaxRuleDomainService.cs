using DBC.Infrastructure.DataAccess;
using DBC.Infrastructure.DataAccess.Repositories;
using DBC.Infrastructure.Services;
using DBC.Models;
using System;
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
            var taxRule = await _taxRuleRepository.GetWithDetails(taxRuleId);
            if (taxRule == null)
                return "The rule has not been found";

            taxRule.Periods.Add(period);

            await _unitOfWork.CommitAsync();

            return "The rule is added successfully";
        }
    }
}
