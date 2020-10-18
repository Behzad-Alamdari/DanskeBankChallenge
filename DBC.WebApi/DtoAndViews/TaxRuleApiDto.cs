using AutoMapper;
using DBC.Infrastructure.AutoMapperConfigs;
using DBC.Models;

namespace DBC.WebApi.DtoAndViews
{
    public class TaxRuleApiDto : IHaveCustomMappings
    {
        public string Name { get; set; }

        public float Percentage { get; set; }

        public int Priority { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<TaxRuleApiDto, TaxRule>()
                .ForMember(d => d.Municipality, o => o.Ignore())
                .ForMember(d => d.MunicipalityId, o => o.Ignore())
                .ForMember(d => d.Periods, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
