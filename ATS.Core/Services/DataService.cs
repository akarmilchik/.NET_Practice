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

        public DataGenericRepository<ContractEntity> contractRepo;

        public DataService(DataContext context)
        {
            _context = context;

            _mapper = MapperFactory.InitMapper();

            contractRepo = new DataGenericRepository<ContractEntity>(context);
        }

        public void CallToTerminal(int chosenClientId, int targetTerminalId)
        {
            var contract = GetContractByClientId(chosenClientId);

            contract.Terminal.Call(GetTerminalPhoneNumberById(targetTerminalId));

            _context.SaveChanges();
        }

        public void DropOutgoingCall(int chosenClientId, int sourceTerminalId)
        {
            var contract = GetContractByClientId(chosenClientId);

            contract.Terminal.DropIncomingRespond();

            _context.SaveChanges();
        }

        public void DropCall(int chosenClientId)
        {
            var contract = GetContractByClientId(chosenClientId);

            contract.Terminal.DropIncomingRespond();

            _context.SaveChanges();
        }

        public void AnswerCall(int chosenClientId)
        {
            var contract = GetContractByClientId(chosenClientId);

            contract.Terminal.Answer();

            _context.SaveChanges();
        }

        public void ConnectTerminalToPort(int chosenClientId)
        {
            var terminal = GetTerminalByClientId(chosenClientId);

            terminal.ConnectToPort(terminal.ProvidedPort);
        }

        public void DisconnectTerminalFromPort(int chosenClientId)
        {
            var terminal = GetTerminalByClientId(chosenClientId);

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
                TariffPlan = (GetTariffPlans() as SecondMinuteTariffPlan)
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
            var terminal = GetTerminalByClientId(chosenClientId);

           

            return GetCallDetailsByPhoneNumberAndPeriod(terminal.PhoneNumber, startReportDay, lastReportDay);
        }

        public IEnumerable<IClient> GetClients() => _mapper.Map<IEnumerable<ClientEntity>, IEnumerable<Client>>(_context.Clients.AsEnumerable());

        public IEnumerable<IPort> GetUnmappedPorts() => _mapper.Map<IEnumerable<PortEntity>, IEnumerable<Port>>(_context.Ports.Where(p => p.PortState == PortState.Disabled).AsEnumerable());

        public IEnumerable<IContract> GetContracts() => _mapper.Map<IEnumerable<ContractEntity>, IEnumerable<Contract>>(_context.Contracts.AsEnumerable());

        public IEnumerable<ITerminal> GetUnmappedTerminals() => _mapper.Map<IEnumerable<TerminalEntity>, IEnumerable<Terminal>>(_context.Terminals.Where(t => t.ProvidedPort_Id == 0).AsEnumerable());

        public IClient GetClientById(int clientId) => _mapper.Map<ClientEntity, Client>(_context.Clients.Where(c => c.Id == clientId).FirstOrDefault());

        public IContract GetContractByClientId(int clientId) => _mapper.Map<ContractEntity, Contract>(_context.Contracts.Where(c => c.Client_Id == clientId).FirstOrDefault());

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

        public IEnumerable<ITariffPlan> GetTariffPlans() => _mapper.Map<IEnumerable<SecondMinuteTariffPlanEntity>, IEnumerable<SecondMinuteTariffPlan>>(_context.TariffPlans.AsEnumerable());

        public ITariffPlan GetTariffPlanByClientId(int clientId) => _mapper.Map<SecondMinuteTariffPlanEntity, SecondMinuteTariffPlan>(_context.TariffPlans.Where(t => t.Id == GetTariffPlanIdByClientId(clientId)).FirstOrDefault());
      
        public IEnumerable<ICallDetails> GetCallDetailsByPhoneNumberAndPeriod(string phoneNumber, DateTime startDate, DateTime lastDate) => _mapper
            .Map<IEnumerable<CallDetailsEntity>, IEnumerable<CallDetails>>(_context.CallsDetails.
            Where(c => c.Source == phoneNumber && c.StartedTime >= startDate && c.StartedTime <= lastDate));
    }
}