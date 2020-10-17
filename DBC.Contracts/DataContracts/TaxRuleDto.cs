using AutoMapper;
using DBC.Infrastructure.AutoMapperConfigs;
using DBC.Models;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DBC.Contracts.DataContracts
{
    [DataContract]
    public class TaxRuleDto : IHaveCustomMappings
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public float Percentage { get; set; }

        [DataMember]
        public int Priority { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<TaxRuleDto, TaxRule>()
                .ForMember(d => d.Municipality, o => o.Ignore())
                .ForMember(d => d.MunicipalityId, o => o.Ignore())
                .ForMember(d => d.Periods, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
