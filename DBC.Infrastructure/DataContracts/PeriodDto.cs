using AutoMapper;
using DBC.Infrastructure.AutoMapperConfigs;
using DBC.Models;
using System;
using System.Runtime.Serialization;

namespace DBC.Infrastructure.DataContracts
{
    [DataContract]
    public class PeriodDto : IHaveCustomMappings
    {
        [DataMember]
        public DateTime From { get; set; }

        [DataMember]
        public DateTime To { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PeriodDto, Period>()
                .ForMember(d => d.TaxRule, o => o.Ignore())
                .ForMember(d => d.TaxRuleId, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
