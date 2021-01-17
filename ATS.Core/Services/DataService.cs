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
using ATS.DAL.Repository;
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

        public Station station;

        public DataRepository<ContractEntity> contractRepo;

        public DataRepository<TerminalEntity> terminalRepo;

        public DataRepository<PortEntity> portRepo;

        public DataService(DataContext context)
        {
            _context = context;

            _mapper = MapperFactory.InitMapper();

            contractRepo = new DataRepository<ContractEntity>(context);

            terminalRepo = new DataRepository<TerminalEntity>(context);

            portRepo = new DataRepository<PortEntity>(context);
        }

        public void CallToTerminal(int chosenClientId, int targetTerminalId)
        {
            var contract = GetContractByClientId(chosenClientId);

            station = InitStation();

            SubscribeEntities(ref contract);

            contract.Terminal.Call(GetTerminalPhoneNumberById(targetTerminalId));

            UpdateEntites(ref contract);

            DisposeEntities(ref contract);
        }

        public void DropOutgoingCall(int chosenClientId, int sourceTerminalId)
        {
            var contract = GetContractByClientId(chosenClientId);

            station = InitStation();

            SubscribeEntities(ref contract);

            contract.Terminal.DropIncomingRespond();

            UpdateEntites(ref contract);

            DisposeEntities(ref contract);
        }

        public void DropCall(int chosenClientId)
        {
            var contract = GetContractByClientId(chosenClientId);

            station = InitStation();

            SubscribeEntities(ref contract);

            contract.Terminal.DropIncomingRespond();

            UpdateEntites(ref contract);

            DisposeEntities(ref contract);
        }

        public void AnswerCall(int chosenClientId)
        {
            var contract = GetContractByClientId(chosenClientId);

            station = InitStation();

            SubscribeEntities(ref contract);

            contract.Terminal.Answer();

            UpdateEntites(ref contract);

            DisposeEntities(ref contract);
        }

        public void ConnectTerminalToPort(int chosenClientId)
        {
            var terminal = GetTerminalByClientId(chosenClientId);

            terminal.ProvidedPort.SubscribeToTerminalEvents(terminal);

            terminal.ConnectToPort(terminal.ProvidedPort);

            terminalRepo.Update(_mapper.Map <Terminal, TerminalEntity>(terminal as Terminal));

            portRepo.Update(_mapper.Map<Port, PortEntity>(terminal.ProvidedPort));

            terminal.ProvidedPort.Dispose();

            terminal.Dispose();
        }

        public void DisconnectTerminalFromPort(int chosenClientId)
        {
            var terminal = GetTerminalByClientId(chosenClientId);

            terminal.ProvidedPort.SubscribeToTerminalEvents(terminal);

            terminal.DisconectFromPort(terminal.ProvidedPort);

            terminalRepo.Update(_mapper.Map<Terminal, TerminalEntity>(terminal as Terminal));

            portRepo.Update(_mapper.Map<Port, PortEntity>(terminal.ProvidedPort));

            terminal.ProvidedPort.Dispose();

            terminal.Dispose();
        }

        public void ConcludeContract(int clientId, int portId, int terminalId, DateTime closeDate)
        {
            var contract = new Contract
            {
                ContractStartDate = DateTime.Today,
                ContractCloseDate = closeDate,
                Client = GetClientById(clientId) as Client,
                Terminal = GetTerminalById(terminalId) as Terminal,
                TariffPlan = GetTariffPlans().FirstOrDefault() as SecondMinuteTariffPlan
            };


            contract.Terminal.SubscribeToContractEvents(contract);

            contract.OnContractConcluded(contract.Terminal);

            contractRepo.Add(_mapper.Map<Contract, ContractEntity>(contract));

            contract.Terminal.Dispose();
        }

        public decimal CalculateMonthCallCost(int chosenClientId, IEnumerable<ICallDetails> callsDetails)
        {
            var contract = GetContractByClientId(chosenClientId);

            return contract.TariffPlan.CalculateCallCost(callsDetails);
        }

        public IEnumerable<ICallDetails> GetCallDetailsInPeriod(int chosenClientId, DateTime startReportDay, DateTime lastReportDay)
        {
            var terminal = GetTerminalByClientId(chosenClientId);

            return GetCallDetailsByPhoneNumberAndPeriod(terminal.PhoneNumber, startReportDay, lastReportDay);
        }

        public void SubscribeEntities(ref IContract contract)
        {
            station.SubscribeToPortEvents(contract.Terminal.ProvidedPort);

            station.SubscribeToTerminalEvents(contract.Terminal);

            contract.Terminal.SubscribeToContractEvents(contract);

            contract.Terminal.ProvidedPort.SubscribeToTerminalEvents(contract.Terminal);
        }

        public void UpdateEntites(ref IContract contract)
        {
            contractRepo.Update(_mapper.Map<Contract, ContractEntity>(contract as Contract));

            terminalRepo.Update(_mapper.Map<Terminal, TerminalEntity>(contract.Terminal));

            portRepo.Update(_mapper.Map<Port, PortEntity>(contract.Terminal.ProvidedPort));
        }

        public void DisposeEntities(ref IContract contract)
        {
            contract.Terminal.ProvidedPort.Dispose();

            contract.Terminal.Dispose();

            station.Dispose();
        }

        public IEnumerable<IClient> GetClients() => _mapper.Map<IEnumerable<ClientEntity>, IEnumerable<Client>>(_context.Clients.AsEnumerable());

        public IEnumerable<IPort> GetUnmappedPorts() => _mapper.Map<IEnumerable<PortEntity>, IEnumerable<Port>>(_context.Ports.Where(p => p.PortState == PortState.Disabled).AsEnumerable());

        public IEnumerable<IContract> GetContracts() => _mapper.Map<IEnumerable<ContractEntity>, IEnumerable<Contract>>(_context.Contracts.AsEnumerable());

        public IEnumerable<ITerminal> GetUnmappedTerminals() => _mapper.Map<IEnumerable<TerminalEntity>, IEnumerable<Terminal>>(_context.Terminals.Where(t => t.ProvidedPort_Id == 0).AsEnumerable());

        public IClient GetClientById(int clientId) => _mapper.Map<ClientEntity, Client>(_context.Clients.FirstOrDefault(c => c.Id == clientId));

        public IContract GetContractByClientId(int clientId) => _mapper.Map<ContractEntity, Contract>(_context.Contracts.FirstOrDefault(c => c.Client_Id == clientId));

        public int GetTerminalIdByClientId(int clientId) => _context.Contracts.Where(c => c.Client_Id == clientId).Select(c => c.Terminal_Id).FirstOrDefault();

        public int GetPortIdByTerminalId(int terminalId) => _context.Terminals.Where(t => t.Id == terminalId).Select(t => t.ProvidedPort_Id).FirstOrDefault();

        public int GetTariffPlanIdByClientId(int clientId) => _context.Contracts.Where(c => c.Client_Id == clientId).Select(c => c.TariffPlan_Id).FirstOrDefault();

        public string GetTerminalPhoneNumberById(int terminalId) => _context.Terminals.Where(t => t.Id == terminalId).Select(t => t.PhoneNumber).FirstOrDefault();

        public IPort GetPortByClientId(int clientId)
        {
            var terminalId = GetTerminalIdByClientId(clientId);

            return _mapper.Map<PortEntity, Port>(_context.Ports.FirstOrDefault(p => p.Id == GetPortIdByTerminalId(terminalId)));
        }

        public ITerminal GetTerminalByClientId(int clientId) => _mapper.Map<TerminalEntity, Terminal>(_context.Terminals.FirstOrDefault(t => t.Id == GetTerminalIdByClientId(clientId)));

        public ITerminal GetTerminalById(int terminalId) => _mapper.Map<TerminalEntity, Terminal>(_context.Terminals.FirstOrDefault(t => t.Id == terminalId));

        public IEnumerable<ITariffPlan> GetTariffPlans() => _mapper.Map<IEnumerable<SecondMinuteTariffPlanEntity>, IEnumerable<SecondMinuteTariffPlan>>(_context.TariffPlans.AsEnumerable());

        public ITariffPlan GetTariffPlanByClientId(int clientId) => _mapper.Map<SecondMinuteTariffPlanEntity, SecondMinuteTariffPlan>(_context.TariffPlans.FirstOrDefault(t => t.Id == GetTariffPlanIdByClientId(clientId)));

        public IEnumerable<ICallDetails> GetCallDetailsByPhoneNumberAndPeriod(string phoneNumber, DateTime startDate, DateTime lastDate) => _mapper
            .Map<IEnumerable<CallDetailsEntity>, IEnumerable<CallDetails>>(_context.CallsDetails.
            Where(c => c.Source == phoneNumber && c.StartedTime >= startDate && c.StartedTime <= lastDate));

        public Station InitStation() => new Station(_mapper.Map<IEnumerable<TerminalEntity>, IEnumerable<Terminal>>(_context.Terminals.AsEnumerable()).ToList(),
                               _mapper.Map<IEnumerable<PortEntity>, IEnumerable<Port>>(_context.Ports.AsEnumerable()).ToList(),
                               _mapper.Map<IEnumerable<ContractEntity>, IEnumerable<Contract>>(_context.Contracts.AsEnumerable()).ToList(),
                               _mapper.Map<IEnumerable<SecondMinuteTariffPlanEntity>, IEnumerable<SecondMinuteTariffPlan>>(_context.TariffPlans.AsEnumerable()).ToList());
        
    }
}