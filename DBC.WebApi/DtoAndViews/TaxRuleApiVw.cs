using AutoMapper;
using DBC.Infrastructure.AutoMapperConfigs;
using DBC.Models;
using System;
using System.Collections.Generic;

namespace DBC.WebApi.DtoAndViews
{
    public class TaxRuleApiVw : IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Percentage { get; set; }

        public int Priority { get; set; }

        public ICollection<PeriodApiVw> Periods { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<TaxRule, TaxRuleApiVw>();
        }
    }
}
