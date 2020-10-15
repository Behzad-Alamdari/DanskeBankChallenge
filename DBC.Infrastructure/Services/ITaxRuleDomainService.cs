using DBC.Models;
using System;
using System.Threading.Tasks;

namespace DBC.Infrastructure.Services
{
    public interface ITaxRuleDomainService
    {
        Task<string> AddPeriod(Guid taxRuleId, Period period);
    }
}
