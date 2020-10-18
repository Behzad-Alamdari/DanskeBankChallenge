using DBC.Infrastructure.AutoMapperConfigs;
using DBC.Models;
using System;

namespace DBC.WebApi.DtoAndViews
{
    public class MunicipalityApiVw : IMapFrom<Municipality>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
