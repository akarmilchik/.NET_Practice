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
            new PortEntity { PortState = PortState.Enabled },
            new PortEntity { PortState = PortState.Enabled }
        };

        private static readonly List<TerminalEntity> Terminals = new List<TerminalEntity>
        {
            new TerminalEntity { PhoneNumber = "100", IsOnline = false, ProvidedPort_Id = 1 },
            new TerminalEntity { PhoneNumber = "222", IsOnline = false },
            new TerminalEntity { PhoneNumber = "300", IsOnline = false },
            new TerminalEntity { PhoneNumber = "440", IsOnline = false }
        };

        private static readonly List<ContractEntity> Contracts = new List<ContractEntity>
        {
            new ContractEntity { Client_Id = 1, ContractStartDate = new DateTime(2020, 10, 01), ContractCloseDate = new DateTime(2020, 12, 31), Terminal_Id = 1, TariffPlan_Id = 1 }
        };

        private static readonly List<StationEntity> Stations = new List<StationEntity>
        {
            new StationEntity { Name = "A1 Station" }
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