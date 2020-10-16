using DBC.Models;
using System;
using System.Threading.Tasks;

namespace DBC.Infrastructure.Domains
{
    public interface ITaxRuleDomainService
    {
        Task<string> AddPeriod(Guid taxRuleId, Period period);
    }
}