using DBC.Infrastructure.DataAccess;
using DBC.Infrastructure.DataAccess.Repositories;
using DBC.Infrastructure.Domains;
using DBC.Models;
using System;
using System.Threading.Tasks;

namespace DBC.Domain.Services
{
    public class TaxRulePeriodDomainService : ITaxRulePeriodDomainService
    {
        private readonly IUnitOfWork _unitOfWrok;
        private readonly ITaxRulePeriodRepository _taxRulePeriodRepository;
        private readonly ITaxRuleRepository _taxRuleRepository;

        public TaxRulePeriodDomainService(IUnitOfWork unitOfWrok,
            ITaxRulePeriodRepository taxRulePeriodRepository,
            ITaxRuleRepository taxRuleRepository)
        {
            _unitOfWrok = unitOfWrok;
            _taxRulePeriodRepository = taxRulePeriodRepository;
            _taxRuleRepository = taxRuleRepository;
        }

        public async Task<(Period, string)> AddPeriod(Guid taxRuleId, Period period)
        {
            if (!(await _taxRuleRepository.Exist(taxRuleId)))
                return (null, "The tax rule id can not be found");

            period.TaxRuleId = taxRuleId;
            _taxRulePeriodRepository.Add(period);

            await _unitOfWrok.CommitAsync();

            return (period, null);
        }

        public async Task<(Period, string)> EditPeriod(Guid id, Period period)
        {
            var periodDatabase = await _taxRulePeriodRepository.GetAsNoTrackingAsync(id);
            if (periodDatabase == null)
                return (null, "The provided period id has not been found");

            period.Id = id;
            period.TaxRuleId = periodDatabase.TaxRuleId;

            _taxRulePeriodRepository.Edit(period);

            await _unitOfWrok.CommitAsync();

            return (period, null);
        }

        public async Task<string> DeletePeriod(Guid id)
        {
            var period = await _taxRulePeriodRepository.GetAsync(id);
            if (period == null)
                return "The provided period id has not been found";

            _taxRulePeriodRepository.Delete(period);

            await _unitOfWrok.CommitAsync();

            return null;
        }
    }
}
