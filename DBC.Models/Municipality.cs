using System.Collections.Generic;

namespace DBC.Models
{
    public class Municipality : Entity
    {
        public Municipality()
        {
            Name = string.Empty;
            TaxRules = new List<TaxRule>();
        }

        public string Name { get; set; }
        public ICollection<TaxRule> TaxRules { get; set; }
    }
}
