using AutoMapper;
using DBC.Infrastructure.AutoMapperConfigs;
using DBC.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DBC.Infrastructure.DataContracts
{
    [DataContract]
    public class TaxRuleVw : IHaveCustomMappings
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public float Percentage { get; set; }

        [DataMember]
        public int Priority { get; set; }

        [DataMember]
        public ICollection<PeriodVw> Periods { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<TaxRule, TaxRuleVw>();
        }
    }
}
