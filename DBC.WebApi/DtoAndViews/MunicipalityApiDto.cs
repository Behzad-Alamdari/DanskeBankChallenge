using DBC.Infrastructure.AutoMapperConfigs;
using DBC.Models;

namespace DBC.WebApi.DtoAndViews
{
    public class MunicipalityapiDto : IMapTo<Municipality>
    {
        public string Name { get; set; }
    }
}
