using AutoMapper;
using DBC.Infrastructure.AutoMapperConfigs;
using DBC.Models;
using System;

namespace DBC.WebApi.DtoAndViews
{
    public class PeriodApiDto : IHaveCustomMappings
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PeriodApiDto, Period>()
                .ForMember(d => d.TaxRule, o => o.Ignore())
                .ForMember(d => d.TaxRuleId, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
