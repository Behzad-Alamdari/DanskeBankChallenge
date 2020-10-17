using DBC.Infrastructure.Utilities;
using DBC.Models;
using System;
using System.Linq;

namespace DBC.Domain.Utilities
{
    public class TaxCanculator : ITaxCanculator
    {
        public (float, string) CalculateTaxFor(Municipality municipality, DateTime date)
        {
            var rules = municipality.TaxRules
                .OrderByDescending(t => t.Priority);

            var taxDate = date.Date;

            foreach (var rule in rules)
            {
                if (rule.Periods == null)
                    continue;

                foreach (var period in rule.Periods)
                {
                    if (period.From.Date <= taxDate && period.To.Date.AddDays(1) > taxDate)
                        return (rule.Percentage, null);
                }
            }

            return (int.MinValue, Messages.NoTaxRuleForDate);
        }
    }
}
