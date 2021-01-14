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
            CreateMap<Client, ClientEntity>();

            CreateMap<Contract, ContractEntity>()
                .ForMember(c => c.Client_Id, opt => opt.MapFrom(ce => ce.Client.Id))
                .ForMember(c => c.Terminal_Id, opt => opt.MapFrom(ce => ce.Terminal.Id))
                .ForMember(c => c.TariffPlan_ID, opt => opt.MapFrom(ce => ce.TariffPlan.Id));


            CreateMap<SecondMinuteTariffPlan, SecondMinuteTariffPlanEntity>();

            CreateMap<CallDetails, CallDetailsEntity>();

            CreateMap<OutgoingRequest, OutgoingRequestEntity>().IncludeBase<Request, RequestEntity>();

            CreateMap<Port, PortEntity>();
                
            CreateMap<Request, RequestEntity>();
                
            CreateMap<Respond, RespondEntity>()
                .ForMember(r => r.Request_Id, opt => opt.MapFrom(r => r.Request.Id));

            CreateMap<Station, StationEntity>();

            CreateMap<Terminal, TerminalEntity>()
                .ForMember(p => p.ProvidedPort_Id, opt => opt.MapFrom(p => p.ProvidedPort.Id)); ;
        }
    }
}
