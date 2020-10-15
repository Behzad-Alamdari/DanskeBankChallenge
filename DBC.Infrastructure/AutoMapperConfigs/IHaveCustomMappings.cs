using AutoMapper;

namespace DBC.Infrastructure.AutoMapperConfigs
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}