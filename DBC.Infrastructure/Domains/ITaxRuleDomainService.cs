using DBC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBC.Infrastructure.Domains
{
    public interface ITaxRuleDomainService
    {
        Task<Guid> AddTaxRuleAsync(Guid municipalityId, TaxRule rule);
        Task<string> EditTaxRuleAsync(Guid taxRuleId, TaxRule rule);
        Task<List<Period>> GetTaxRulePeriodsAsync(Guid taxRuleId);
    }
}