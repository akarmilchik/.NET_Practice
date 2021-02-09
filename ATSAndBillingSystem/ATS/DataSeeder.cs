using ATS.DAL;
using ATS.DAL.Constants;
using ATS.DAL.ModelsEntities;
using ATS.DAL.ModelsEntities.Billing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATS
{
    public class DataSeeder
    {
        private readonly DataContext _context;

        public DataSeeder(DataContext context)
        {
            _context = context;
        }

        private static readonly List<SecondMinuteTariffPlanEntity> TariffPlans = new List<SecondMinuteTariffPlanEntity>
        {
            new SecondMinuteTariffPlanEntity { Name = "Every second minute free", MinuteCost = 0.1m }
        };

        private static readonly List<ClientEntity> Clients = new List<ClientEntity>
        {
            new ClientEntity { FirstName = "Alex", LastName = "Karm" },
            new ClientEntity { FirstName = "Jeff", LastName = "Bezos" },
            new ClientEntity { FirstName = "Elvis", LastName = "Presley" },
            new ClientEntity { FirstName = "Marty", LastName = "McFly" },
            new ClientEntity { FirstName = "Scarlett", LastName = "Johansson" }
        };

        private static readonly List<PortEntity> Ports = new List<PortEntity>
        {
            new PortEntity { PortState = PortState.Enabled },
            new PortEntity { PortState = PortState.Enabled },
            new PortEntity { PortState = PortState.Disabled },
            new PortEntity { PortState = PortState.Disabled }
        };

        private static readonly List<TerminalEntity> Terminals = new List<TerminalEntity>
        {
            new TerminalEntity { PhoneNumber = "100", IsOnline = false, ProvidedPort_Id = 4 },
            new TerminalEntity { PhoneNumber = "222", IsOnline = false, ProvidedPort_Id = 2 },
            new TerminalEntity { PhoneNumber = "300", IsOnline = false },
            new TerminalEntity { PhoneNumber = "440", IsOnline = false }
        };

        private static readonly List<ContractEntity> Contracts = new List<ContractEntity>
        {
            new ContractEntity { Client_Id = 1, ContractStartDate = new DateTime(2020, 10, 01), ContractCloseDate = new DateTime(2021, 06, 30), Terminal_Id = 4, TariffPlan_Id = 1 },
            new ContractEntity { Client_Id = 2, ContractStartDate = new DateTime(2021, 01, 01), ContractCloseDate = new DateTime(2020, 12, 31), Terminal_Id = 3, TariffPlan_Id = 1 }
        };

        private static readonly List<StationEntity> Stations = new List<StationEntity>
        {
            new StationEntity { Name = "A1 Station" }
        };

        private static readonly List<CallDetailsEntity> CallDetails = new List<CallDetailsEntity>
        {
            new CallDetailsEntity { StartedTime = new DateTime(2021, 01, 05, 15, 0, 0), DurationTime = new TimeSpan(0, 5, 36), Cost = 33.6m, Source = "100", Target = "222" },
            new CallDetailsEntity { StartedTime = new DateTime(2021, 01, 12, 09, 26, 0), DurationTime = new TimeSpan(0, 1, 15), Cost = 7.5m, Source = "100", Target = "222" },
            new CallDetailsEntity { StartedTime = new DateTime(2021, 01, 26, 23, 40, 15), DurationTime = new TimeSpan(0, 14, 48), Cost = 88.8m, Source = "100", Target = "222" },
            new CallDetailsEntity { StartedTime = new DateTime(2021, 01, 14, 15, 0, 0), DurationTime = new TimeSpan(0, 14, 16), Cost = 85.6m, Source = "222", Target = "100" },
            new CallDetailsEntity { StartedTime = new DateTime(2021, 01, 03, 19, 45, 30), DurationTime = new TimeSpan(0, 3, 50), Cost = 23m, Source = "222", Target = "100" },
        };

        private static readonly List<RequestEntity> Requests = new List<RequestEntity>
        {
            new RequestEntity { SourcePhoneNumber = "100", State = RequestRespondState.Accept },
            new RequestEntity { SourcePhoneNumber = "222", State = RequestRespondState.Accept }
        };

        private static readonly List<OutgoingRequestEntity> OutgoingRequests = new List<OutgoingRequestEntity>
        {
            new OutgoingRequestEntity { TargetPhoneNumber = "222" },
            new OutgoingRequestEntity { TargetPhoneNumber = "100" },
        };

        private static readonly List<RespondEntity> Responds = new List<RespondEntity>
        {
            new RespondEntity { Request_Id = 1, SourcePhoneNumber = "100", State = RequestRespondState.Accept },
            new RespondEntity { Request_Id = 2, SourcePhoneNumber = "222", State = RequestRespondState.Accept }
        };

        public void SeedData()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.TariffPlans.Any())
                {
                    _context.TariffPlans.AddRange(TariffPlans);
                }

                if (!_context.Clients.Any())
                {
                    _context.Clients.AddRange(Clients);
                }

                if (!_context.Terminals.Any())
                {
                    _context.Terminals.AddRange(Terminals);
                }

                if (!_context.Ports.Any())
                {
                    _context.Ports.AddRange(Ports);
                }

                if (!_context.Contracts.Any())
                {
                    _context.Contracts.AddRange(Contracts);
                }
                if (!_context.Stations.Any())
                {
                    _context.Stations.AddRange(Stations);
                }
                if (!_context.CallsDetails.Any())
                {
                    _context.CallsDetails.AddRange(CallDetails);
                }
                if (!_context.Requests.Any())
                {
                    _context.Requests.AddRange(Requests);
                }
                if (!_context.OutgoingRequests.Any())
                {
                    _context.OutgoingRequests.AddRange(OutgoingRequests);
                }
                if (!_context.Responds.Any())
                {
                    _context.Responds.AddRange(Responds);
                }

                _context.SaveChanges();
            }
        }
    }
}