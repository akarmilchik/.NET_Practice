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
            new SecondMinuteTariffPlanEntity { Id = 1, Name = "Every second minute free", MinuteCost = 0.1m, CostCalculator_Id = 1 }
        };

        private static readonly List<ClientEntity> Clients = new List<ClientEntity>
        {
            new ClientEntity { Id = 1, FirstName = "Alex", LastName = "Karm" },
            new ClientEntity { Id = 2, FirstName = "Jeff", LastName = "Bezos" },
            new ClientEntity { Id = 3, FirstName = "Elvis", LastName = "Presley" },
            new ClientEntity { Id = 4, FirstName = "Marty", LastName = "McFly" },
            new ClientEntity { Id = 5, FirstName = "Scarlett", LastName = "Johansson" }
        };

        private static readonly List<TerminalEntity> Terminals = new List<TerminalEntity>
        {
            new TerminalEntity { Id = 1 ,PhoneNumber = "100" },
            new TerminalEntity { Id = 2, PhoneNumber = "222" },
            new TerminalEntity { Id = 3, PhoneNumber = "300" },
            new TerminalEntity { Id = 4, PhoneNumber = "440" }
        };

        private static readonly List<PortEntity> Ports = new List<PortEntity>
        {
            new PortEntity { Id = 1, PortState = PortState.Enabled },
            new PortEntity { Id = 2, PortState = PortState.Disabled },
            new PortEntity { Id = 3, PortState = PortState.Calling },
            new PortEntity { Id = 4, PortState = PortState.Enabled }
        };

        private static readonly List<ContractEntity> Contracts = new List<ContractEntity>
        {
            new ContractEntity { Id = 1, Client_Id = Clients[1].Id, ContractStartDate = new DateTime(2020, 10, 01), ContractCloseDate = new DateTime(2020, 12, 31), Terminal_Id = Terminals[3].Id },
            new ContractEntity { Id = 2, Client_Id = Clients[3].Id, ContractStartDate = new DateTime(2020, 11, 01), ContractCloseDate = new DateTime(2021, 04, 30), Terminal_Id = Terminals[4].Id },
            new ContractEntity { Id = 3, Client_Id = Clients[2].Id, ContractStartDate = new DateTime(2020, 12, 15), ContractCloseDate = new DateTime(2021, 03, 15), Terminal_Id = Terminals[2].Id },
            new ContractEntity { Id = 4, Client_Id = Clients[4].Id, ContractStartDate = new DateTime(2020, 09, 01), ContractCloseDate = new DateTime(2021, 01, 31), Terminal_Id = Terminals[1].Id }
        };

        private static readonly List<StationEntity> Stations = new List<StationEntity>
        {
            new StationEntity { Id = 1, Name = "A1 Station" }
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

                _context.SaveChanges();
            }
        }
    }
}