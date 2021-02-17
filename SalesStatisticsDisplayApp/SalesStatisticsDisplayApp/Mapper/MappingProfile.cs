using AutoMapper;
using SalesStatistics.App.Controllers.Api.Models;
using SalesStatistics.DAL.Models;
using System.Linq;

namespace SalesStatistics.App.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResource>()
                .ForMember(pr => pr.Id, opt => opt.MapFrom(p => p.Id))
                .ForMember(pr => pr.Name, opt => opt.MapFrom(p => p.Name))
                .ForMember(pr => pr.Cost, opt => opt.MapFrom(p => p.Cost))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Client, ClientResource>()
                .ForMember(cr => cr.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(cr => cr.FirstName, opt => opt.MapFrom(c => c.FirstName))
                .ForMember(cr => cr.LastName, opt => opt.MapFrom(c => c.LastName))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Order, OrderResource>()
                .ForMember(or => or.Id, opt => opt.MapFrom(o => o.Id))
                .ForMember(or => or.ClientId, opt => opt.MapFrom(o => o.ClientId))
                .ForMember(or => or.ProductId, opt => opt.MapFrom(o => o.ProductId))
                .ForMember(or => or.Date, opt => opt.MapFrom(o => o.Date))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
