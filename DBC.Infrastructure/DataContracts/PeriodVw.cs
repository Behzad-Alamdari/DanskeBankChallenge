using AutoMapper;
using DBC.Infrastructure.AutoMapperConfigs;
using System;
using System.Runtime.Serialization;

namespace DBC.Infrastructure.DataContracts
{
    [DataContract]
    public class PeriodVw : IHaveCustomMappings
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public DateTime From { get; set; }

        [DataMember]
        public DateTime To { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Models.Period, PeriodVw>();
        }
    }
}
