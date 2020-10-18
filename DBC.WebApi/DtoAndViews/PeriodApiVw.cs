using AutoMapper;
using DBC.Infrastructure.AutoMapperConfigs;
using System;

namespace DBC.WebApi.DtoAndViews
{
    public class PeriodApiVw : IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Models.Period, PeriodApiVw>();
        }
    }
}
