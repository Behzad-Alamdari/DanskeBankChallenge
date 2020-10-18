using AutoMapper;
using DBC.Infrastructure.AutoMapperConfigs;
using DBC.Models;

namespace DBC.WebApi.DtoAndViews
{
    public class PaginationFilterDto: IHaveCustomMappings
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PaginationFilterDto, Pagination>();
        }
    }
}
