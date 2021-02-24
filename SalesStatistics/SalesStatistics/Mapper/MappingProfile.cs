using AutoMapper;
using SalesStatistics.Controllers.Api.Models;
using SalesStatistics.DAL.Models;

namespace SalesStatistics.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResource>();
            CreateMap<Client, ClientResource>();
            CreateMap<Order, OrderResource>();
        }
    }
}
