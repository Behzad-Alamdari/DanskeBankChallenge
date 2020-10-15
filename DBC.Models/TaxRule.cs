using System;
using System.Collections.Generic;

namespace DBC.Models
{
    public class TaxRule : Entity
    {
        public TaxRule()
        {
            Name = string.Empty;
            Periods = new List<Period>();
        }

        public Guid MunicipalityId { get; set; }
        public Municipality Municipality { get; set; }

        public string Name { get; set; }
        public float Percentage { get; set; }
        public int Priority { get; set; }
        public ICollection<Period> Periods { get; set; }
    }
}
