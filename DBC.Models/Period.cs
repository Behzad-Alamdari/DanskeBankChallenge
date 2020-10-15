using System;

namespace DBC.Models
{
    public class Period : Entity
    {
        public Guid TaxRuleId { get; set; }
        public TaxRule TaxRule { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
