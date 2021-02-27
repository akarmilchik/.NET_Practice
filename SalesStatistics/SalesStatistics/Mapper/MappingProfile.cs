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
            CreateMap<Client, ClientResource>().ForMember(c => c.CountryName, opt => opt.MapFrom(src => src.Country.Name));
            CreateMap<Order, OrderResource>()
                .ForMember(o => o.ClientFirstName, opt => opt.MapFrom(src => src.Client.FirstName))
                .ForMember(o => o.ClientLastName, opt => opt.MapFrom(src => src.Client.LastName))
                .ForMember(o => o.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(o => o.ProductCost, opt => opt.MapFrom(src => src.Product.Cost));

            CreateMap<Country, CountryResource>();
        }
    }
}
