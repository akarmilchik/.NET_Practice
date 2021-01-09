using ATS.DAL;
using ATS.DAL.Constants;
using ATS.DAL.Models;
using ATS.DAL.Models.Billing;
using ATS.DAL.Models.TariffPlans;
using System;
using System.Collections.Generic;

namespace ATS
{
    public class DataSeeder
    {
        private readonly DataContext _context;

        public DataSeeder(DataContext context)
        {
            _context = context;
        }

        private static readonly List<SecondMinuteTariffPlan> TariffPlans = new List<SecondMinuteTariffPlan>
        {
            new SecondMinuteTariffPlan(1, "Every second minute free", 0.1m)
        };

        private static readonly List<Client> Clients = new List<Client>
        {
            new Client { Id = 1, FirstName = "Alex", LastName = "Karm"},
            new Client { Id = 2, FirstName = "Jeff", LastName = "Bezos"},
            new Client { Id = 3, FirstName = "Elvis", LastName = "Presley"},
            new Client { Id = 4, FirstName = "Marty", LastName = "McFly"},
            new Client { Id = 5, FirstName = "Scarlett", LastName = "Johansson"}
        };

        private static readonly List<Terminal> Terminals = new List<Terminal>
        {
            new Terminal {Id = 1 ,PhoneNumber = "100" },
            new Terminal {Id = 2, PhoneNumber = "222" },
            new Terminal {Id = 3, PhoneNumber = "300" },
            new Terminal {Id = 4, PhoneNumber = "440" }
        };

        private static readonly List<Port> Ports = new List<Port>
        {
            new Port { PortState = PortState.Free },
            new Port { PortState = PortState.Free },
            new Port { PortState = PortState.Free },
            new Port { PortState = PortState.Free }
        };

        private static readonly List<Contract> Contracts = new List<Contract>
        {
            new Contract {Id = 1, Client = Clients[1], ContractStartDate = new DateTime(2020, 10, 01), ContractCloseDate = new DateTime(2020, 12, 31), Terminal = Terminals[3] },
            new Contract {Id = 2, Client = Clients[3], ContractStartDate = new DateTime(2020, 11, 01), ContractCloseDate = new DateTime(2021, 04, 30), Terminal = Terminals[4] },
            new Contract {Id = 3, Client = Clients[2], ContractStartDate = new DateTime(2020, 12, 15), ContractCloseDate = new DateTime(2021, 03, 15), Terminal = Terminals[2] },
            new Contract {Id = 4, Client = Clients[4], ContractStartDate = new DateTime(2020, 09, 01), ContractCloseDate = new DateTime(2021, 01, 31), Terminal = Terminals[1] }
        };


    }
}

