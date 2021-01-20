using ATS.DAL.Models;
using ATS.DAL.Models.Billing;
using ATS.DAL.Models.Requests;
using ATS.DAL.Models.Responds;
using ATS.DAL.ModelsEntities;
using ATS.DAL.ModelsEntities.Billing;
using AutoMapper;

namespace ATS.Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClientEntity, Client>().ReverseMap();

            CreateMap<ContractEntity, Contract>()
                .ForPath(ce => ce.Client.Id, opt => opt.MapFrom(c => c.Client_Id))
                .ForPath(ce => ce.Terminal.Id, opt => opt.MapFrom(c => c.Terminal_Id))
                .ForPath(ce => ce.TariffPlan.Id, opt => opt.MapFrom(c => c.TariffPlan_Id)).ReverseMap();

            CreateMap<SecondMinuteTariffPlanEntity, SecondMinuteTariffPlan>().ReverseMap();

            CreateMap<CallDetailsEntity, CallDetails>().ReverseMap();

            CreateMap<OutgoingRequestEntity, OutgoingRequest>().IncludeBase<RequestEntity, Request>().ReverseMap();

            CreateMap<PortEntity, Port>().ReverseMap();
                
            CreateMap<RequestEntity, Request>().ReverseMap();
                
            CreateMap<RespondEntity, Respond>()
                .ForPath(r => r.Request.Id, opt => opt.MapFrom(r => r.Request_Id)).ReverseMap();

            CreateMap<StationEntity, Station>().ReverseMap();

            CreateMap<TerminalEntity, Terminal>()
                .ForPath(p => p.ProvidedPort.Id, opt => opt.MapFrom(p => p.ProvidedPort_Id))
                .ForPath(p => p.ServerIncomingRequest.Id, opt => opt.MapFrom(p => p.Request_Id)).ReverseMap();
        }
    }
}
