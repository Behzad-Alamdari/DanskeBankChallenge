using DBC.Models;
using System;
using System.Threading.Tasks;

namespace DBC.Infrastructure.Domains
{
    public interface ITaxRulePeriodDomainService
    {
        Task<(Period, string)> AddPeriod(Guid taxRuleId, Period period);
        Task<(Period, string)> EditPeriod(Guid id, Period period);
        Task<string> DeletePeriod(Guid id);
    }
}
