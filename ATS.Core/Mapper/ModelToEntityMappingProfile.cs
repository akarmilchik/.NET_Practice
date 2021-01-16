using ATS.DAL.Models;
using ATS.DAL.Models.Billing;
using ATS.DAL.Models.Requests;
using ATS.DAL.Models.Responds;
using ATS.DAL.ModelsEntities;
using ATS.DAL.ModelsEntities.Billing;
using AutoMapper;

namespace ATS.Core.Mapper
{
    public class ModelToEntityMappingProfile : Profile
    {
        public ModelToEntityMappingProfile()
        {
            CreateMap<Client, ClientEntity>();

            CreateMap<Contract, ContractEntity>()
                .ForPath(c => c.Client_Id, opt => opt.MapFrom(c => c.Client.Id))
                .ForPath(c => c.Terminal_Id, opt => opt.MapFrom(c => c.Terminal.Id))
                .ForPath(c => c.TariffPlan_Id, opt => opt.MapFrom(c => c.TariffPlan.Id));

            CreateMap<SecondMinuteTariffPlan, SecondMinuteTariffPlanEntity>();

            CreateMap<CallDetails, CallDetailsEntity>();

            CreateMap<OutgoingRequest, OutgoingRequestEntity>().IncludeBase<Request, RequestEntity>();

            CreateMap<Port, PortEntity>();

            CreateMap<Request, RequestEntity>();

            CreateMap<Respond, RespondEntity>()
                .ForPath(r => r.Request_Id, opt => opt.MapFrom(r => r.Request.Id));

            CreateMap<Station, StationEntity>();

            CreateMap<Terminal, TerminalEntity>()
                .ForPath(p => p.ProvidedPort_Id, opt => opt.MapFrom(p => p.ProvidedPort.Id));
        }
    }
}
