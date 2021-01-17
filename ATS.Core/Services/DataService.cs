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

        private DataGenericRepository<ClientEntity> clientRepo;
        public DataGenericRepository<ContractEntity> contractRepo;
        private DataGenericRepository<SecondMinuteTariffPlanEntity> tariffPlanRepo;
        private DataGenericRepository<OutgoingRequestEntity> outgoingRequestRepo;
        private DataGenericRepository<RequestEntity> requestRepo;
        private DataGenericRepository<RespondEntity> respondRepo;
        private DataGenericRepository<CallDetailsEntity> callDetailsRepo;
        private DataGenericRepository<PortEntity> portRepo;
        private DataGenericRepository<StationEntity> stationRepo;
        private DataGenericRepository<TerminalEntity> terimnalRepo;

        public DataService(DataContext context)
        {
            _context = context;
            _mapper = MapperFactory.InitMapper();
            clientRepo = new DataGenericRepository<ClientEntity>(context);
            contractRepo = new DataGenericRepository<ContractEntity>(context);
            tariffPlanRepo = new DataGenericRepository<SecondMinuteTariffPlanEntity>(context);
            outgoingRequestRepo = new DataGenericRepository<OutgoingRequestEntity>(context);
            requestRepo = new DataGenericRepository<RequestEntity>(context);
            respondRepo = new DataGenericRepository<RespondEntity>(context);
            portRepo = new DataGenericRepository<PortEntity>(context);
            stationRepo = new DataGenericRepository<StationEntity>(context);
            terimnalRepo = new DataGenericRepository<TerminalEntity>(context);
            callDetailsRepo = new DataGenericRepository<CallDetailsEntity>(context);
        }

        public void CallToTerminal(int chosenCliendId, int targetTerminalId)
        {
            var contract = GetContractByClientId(chosenCliendId);

            contract.Terminal.Call(GetTerminalPhoneNumberById(targetTerminalId));

            _context.SaveChanges();
        }

        public void DropCall(int chosenCliendId)
        {
            var contract = GetContractByClientId(chosenCliendId);

            contract.Terminal.DropIncomingRespond();

            _context.SaveChanges();
        }

        public void AnswerCall(int chosenCliendId)
        {
            var contract = GetContractByClientId(chosenCliendId);

            contract.Terminal.Answer();

            _context.SaveChanges();
        }

        public void ConnectTerminalToPort(int chosenCliendId)
        {
            var terminal = GetTerminalByClientId(chosenCliendId);

            terminal.ConnectToPort(terminal.ProvidedPort);
        }

        public void DisconnectTerminalFromPort(int chosenCliendId)
        {
            var terminal = GetTerminalByClientId(chosenCliendId);

            terminal.DisconectFromPort(terminal.ProvidedPort);
        }

        public void ConcludeContract(int clientId, int portId, int terminalId, DateTime closeDate)
        {
            var contract = new Contract
            {
                ContractStartDate = DateTime.Today,
                ContractCloseDate = closeDate,
                Client = GetClientById(clientId) as Client,
                Terminal = GetTerminalById(terminalId) as Terminal,
                TariffPlan = GetTariffPlan() as SecondMinuteTariffPlan
            };

            contract.OnContractConcluded(contract.Terminal as Terminal);

            contractRepo.Add(_mapper.Map<Contract, ContractEntity>(contract));
        }

        public decimal CalculateMonthCallCost(int chosenClientId, IEnumerable<ICallDetails> callsDetails)
        {
            var contract = GetContractByClientId(chosenClientId);

            return contract.TariffPlan.CalculateCallCost(callsDetails);
        }

        public IEnumerable<ICallDetails> GetCallDetailsInPeriod(int chosenClientId, DateTime startReportDay, DateTime lastReportDay)
        {
            var contract = GetContractByClientId(chosenClientId);

            var callsDetails = callDetailsRepo.Get().Where(cd => cd.Source == contract.Terminal.PhoneNumber && cd.StartedTime >= startReportDay && cd.StartedTime.AddSeconds(cd.DurationTime.TotalSeconds) <= lastReportDay).AsEnumerable();
            
            return _mapper.Map<IEnumerable<CallDetailsEntity>, IEnumerable<CallDetails>>(callsDetails);
        }

        public void CalculateCost(ICallDetails callDetails)
        {
            if (callDetails == null)
            {
                throw new ArgumentNullException("Source terminal is null");
            }

            var contract = GetActiveContractForTerminal(GetTerminalByPhoneNumber(callDetails.Source), callDetails.StartedTime);

            if (contract == null)
            {
                throw new ArgumentException(String.Format("No active contract is for terminal {0} on {1}", callDetails.Source, callDetails.StartedTime));
            }

            if (contract.TariffPlan == null)
            {
                throw new ArgumentException(String.Format("No plan is active for contract"));
            }

            DateTime periodStart = new DateTime(Math.Max(contract.ContractStartDate.Ticks, callDetails.StartedTime.AddMonths(-1).Ticks));

            var previous = _mapper.Map<IEnumerable<CallDetailsEntity>, IEnumerable<CallDetails>>(callDetailsRepo.Get()
                .Where(c => c.StartedTime >= periodStart &&
                       c.StartedTime.AddSeconds(1) < callDetails.StartedTime));

            callDetails.Cost = contract.TariffPlan.CalculateCallCost(previous);
        }

        protected IContract GetActiveContractForTerminal(ITerminal terminal, DateTime startedTime)
        {
            return _mapper.Map<IEnumerable<ContractEntity>, IEnumerable<Contract>>(contractRepo.Get()).FirstOrDefault();
        }

        public IEnumerable<IClient> GetClients() => _mapper.Map<IEnumerable<ClientEntity>, IEnumerable<Client>>(_context.Clients.AsEnumerable());

        public IEnumerable<ITerminal> GetTerminals() => _mapper.Map<IEnumerable<TerminalEntity>, IEnumerable<Terminal>>(_context.Terminals.AsEnumerable());

        public IEnumerable<IPort> GetPorts() => _mapper.Map<IEnumerable<PortEntity>, IEnumerable<Port>>(_context.Ports.AsEnumerable());

        public IEnumerable<IPort> GetUnmappedPorts() => _mapper.Map<IEnumerable<PortEntity>, IEnumerable<Port>>(_context.Ports.Where(p => p.PortState == PortState.Disabled).AsEnumerable());

        public IEnumerable<IContract> GetContracts() => _mapper.Map<IEnumerable<ContractEntity>, IEnumerable<Contract>>(_context.Contracts.AsEnumerable());

        public IEnumerable<ITerminal> GetUnmappedTerminals() => _mapper.Map<IEnumerable<TerminalEntity>, IEnumerable<Terminal>>(_context.Terminals.Where(t => t.ProvidedPort_Id == 0).AsEnumerable());

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

        public ITerminal GetTerminalByPhoneNumber(string number) => _mapper.Map<TerminalEntity, Terminal>(_context.Terminals.Where(t => t.PhoneNumber == number).FirstOrDefault());

        public ITariffPlan GetTariffPlan() => _mapper.Map<SecondMinuteTariffPlanEntity, SecondMinuteTariffPlan>(_context.TariffPlans.FirstOrDefault());

        public ITariffPlan GetTariffPlanByClientId(int clientId) => _mapper.Map<SecondMinuteTariffPlanEntity, SecondMinuteTariffPlan>(_context.TariffPlans.Where(t => t.Id == GetTariffPlanIdByClientId(clientId)).FirstOrDefault());

        public ITariffPlan GetCallDetails() => _mapper.Map<SecondMinuteTariffPlanEntity, SecondMinuteTariffPlan>(_context.TariffPlans.FirstOrDefault());
    }
}