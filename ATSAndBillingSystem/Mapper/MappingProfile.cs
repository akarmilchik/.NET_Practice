using ATS.DAL.Models;
using ATS.DAL.Models.Billing;
using ATS.DAL.Models.Requests;
using ATS.DAL.Models.Responds;
using ATS.DAL.ModelsEntities;
using ATS.DAL.ModelsEntities.Billing;
using AutoMapper;

namespace ATS.Mapper
{
    public class MappingProfile : Profile
    {
        
        public MappingProfile()
        {
            CreateMap<Client, ClientEntity>();

            CreateMap<Contract, ContractEntity>()
                .ForMember(c => c.Client_Id, opt => opt.MapFrom(ce => ce.Client.Id));

            CreateMap<ContractToTariffPlanBinding, ContractToTariffPlanBindingEntity>()
                .ForMember(c => c.Contract_Id, opt => opt.MapFrom(ce => ce.Contract.Id))
                .ForMember(c => c.TariffPlan_Id, opt => opt.MapFrom(ce => ce.TariffPlan.Id));

            CreateMap<SecondMinuteTariffPlan, SecondMinuteTariffPlanEntity>()
                .ForMember(tp => tp.MinuteCost, opt => opt.MapFrom(tp => tp.MinuteCost));

            CreateMap<CallDetails, CallDetailsEntity>();

            CreateMap<OutgoingRequest, OutgoingRequestEntity>().
                IncludeBase<Request, RequestEntity>();

                //.ForMember(or => or.TargetPhoneNumber, opt => opt.MapFrom(or => or.TargetPhoneNumber))
                //.ForMember(or => or.SourcePhoneNumber, opt => opt.MapFrom(or => ));

            CreateMap<Port, PortEntity>()
                .ForMember(p => p.PortState, opt => opt.MapFrom(p => p.PortState));

            CreateMap<Request, RequestEntity>();
                
            CreateMap<Respond, RespondEntity>()
                .ForMember(r => r.Request_Id, opt => opt.MapFrom(r => r.Request.Id));

            CreateMap<Station, StationEntity>();

            CreateMap<Terminal, TerminalEntity>();

            //mapper.Map<IEnumerable<ClietnEntity>>(Items));
        }
    }
}
