using ATS.Core.Interfaces;
using ATS.Core.Mapper;
using ATS.DAL;
using ATS.DAL.Constants;
using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using ATS.DAL.Models;
using ATS.DAL.Models.Billing;
using ATS.DAL.ModelsEntities;
using ATS.DAL.ModelsEntities.Billing;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATS.Core.Services
{
    public class DataService : IDataService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DataService(DataContext context)
        {
            _context = context;
            _mapper = MapperFactory.InitMapper();
        }



        public void ConnectToTerminal(int chosenCliendId, int targetTerminalId)
        {
            var contract = GetContractByClientId(chosenCliendId);

            contract.Terminal.Call(GetTerminalPhoneNumberById(targetTerminalId));

            _context.SaveChanges();
        }


        public void ConcludeContract(int clientId, int portId, int terminalId, DateTime closeDate)
        {
            var contract = new Contract
            {
                ContractStartDate = DateTime.Today,
                ContractCloseDate = closeDate,
                Client = GetClientById(clientId),
                Terminal = GetTerminalById(terminalId),
                TariffPlan = GetTariffPlan()
            };

            contract.Terminal.RegisterEventHandlersForContract(contract);

            AddContractToDb(contract);
        }


        public void AddContractToDb(Contract contract)
        {
            var contractEntity = _mapper.Map<Contract, ContractEntity>(contract);

            _context.Contracts.Add(contractEntity);

            _context.SaveChanges();
        }

        public void RemoveContractFromDb(Contract contract)
        {
            var contractEntity = _mapper.Map<Contract, ContractEntity>(contract);

            _context.Contracts.Remove(contractEntity);

            _context.SaveChanges();
        }


        public IEnumerable<IClient> GetClients() => _mapper.Map<IEnumerable<ClientEntity>, IEnumerable<Client>>(_context.Clients.AsEnumerable());
        
        public IEnumerable<ITerminal> GetTerminals() => _mapper.Map<IEnumerable<TerminalEntity>, IEnumerable<Terminal>>(_context.Terminals.AsEnumerable());

        public IEnumerable<IPort> GetPorts() => _mapper.Map<IEnumerable<PortEntity>, IEnumerable<Port>>(_context.Ports.AsEnumerable());

        public IEnumerable<IContract> GetContracts() => _mapper.Map<IEnumerable<ContractEntity>, IEnumerable<Contract>>(_context.Contracts.AsEnumerable());

        public IClient GetClientById(int clientId) => _mapper.Map<ClientEntity, Client>(_context.Clients.Where(c => c.Id == clientId).FirstOrDefault());

        public IContract GetContractByClientId(int clientId) => _mapper.Map<ContractEntity, Contract>(_context.Contracts.Where(c => c.Client_Id == clientId).FirstOrDefault());

        public IContract GetContractById(int contractId) => _mapper.Map<ContractEntity, Contract>(_context.Contracts.Where(c => c.Id == contractId).FirstOrDefault());

        public int GetTerminalIdByClientId(int clientId) => _context.Contracts.Where(c => c.Client_Id == clientId).Select(c => c.Terminal_Id).FirstOrDefault();

        public int GetPortIdByTerminalId(int terminalId) => _context.Terminals.Where(t => t.Id == terminalId).Select(t => t.ProvidedPort_Id).FirstOrDefault();

        public int GetTariffPlanIdByClientId(int clientId) => _context.Contracts.Where(c => c.Client_Id == clientId).Select(c => c.TariffPlan_Id).FirstOrDefault();

        public string GetTerminalPhoneNumberById(int terminalId) => _context.Terminals.Where(t => t.Id == terminalId).Select(t => t.PhoneNumber).FirstOrDefault();

        public IPort GetPortByClientId(int clientId)
        {

            var terminalId = GetTerminalIdByClientId(clientId);

            return _mapper.Map<PortEntity, Port>(_context.Ports.Where(p => p.Id == GetPortIdByTerminalId(terminalId)).FirstOrDefault());
        }

        public ITerminal GetTerminalByClientId(int clientId) => _mapper.Map<TerminalEntity, Terminal>(_context.Terminals.Where(t => t.Id == GetTerminalIdByClientId(clientId)).FirstOrDefault());

        public ITerminal GetTerminalById(int terminalId) => _mapper.Map<TerminalEntity, Terminal>(_context.Terminals.Where(t => t.Id == terminalId).FirstOrDefault());

        public ITariffPlan GetTariffPlan() => _mapper.Map<SecondMinuteTariffPlanEntity, SecondMinuteTariffPlan>(_context.TariffPlans.FirstOrDefault());

        public ITariffPlan GetTariffPlanByClientId(int clientId)
        {
            var tariffPlanId = GetTariffPlanIdByClientId(clientId);

            return _mapper.Map<SecondMinuteTariffPlanEntity, SecondMinuteTariffPlan>(_context.TariffPlans.Where(t => t.Id == tariffPlanId).FirstOrDefault());
        }

    }
}
