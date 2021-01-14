using ATS.Core.Interfaces;
using ATS.DAL;
using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using ATS.DAL.Models;
using ATS.DAL.Models.Billing;
using ATS.DAL.ModelsEntities;
using ATS.DAL.ModelsEntities.Billing;
using System.Collections.Generic;
using System.Linq;

namespace ATS.Core.Services
{
    public class DataService : IDataService
    {
        private readonly DataContext _context;
        private readonly AutoMapper.Mapper _mapper;

        public DataService(DataContext context, AutoMapper.Mapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddContractToDb(ContractEntity contract)
        {
            _context.Contracts.Add(contract);

            _context.SaveChanges();
        }

        public void RemoveContractToDb(ContractEntity contract)
        {
            _context.Contracts.Remove(contract);

            _context.SaveChanges();
        }

        public IEnumerable<IClient> GetClients() => _mapper.Map<IEnumerable<ClientEntity>, IEnumerable<Client>>(_context.Clients.AsEnumerable());
        
        public IEnumerable<ITerminal> GetTerminals() => _mapper.Map<IEnumerable<TerminalEntity>, IEnumerable<Terminal>>(_context.Terminals.AsEnumerable());

        public IEnumerable<IContract> GetContracts() => _mapper.Map<IEnumerable<ContractEntity>, IEnumerable<Contract>>(_context.Contracts.AsEnumerable());

        public IClient GetClientById(int clientId) => _mapper.Map<ClientEntity, Client>(_context.Clients.Where(c => c.Id == clientId).FirstOrDefault());

        public IContract GetContractByClientId(int clientId) => _mapper.Map<ContractEntity, Contract>(_context.Contracts.Where(c => c.Client_Id == clientId).FirstOrDefault());

        public int GetTerminalIdByClientId(int clientId) => _context.Contracts.Where(c => c.Client_Id == clientId).Select(c => c.Terminal_Id).FirstOrDefault();
        public int GetPortIdByTerminalId(int terminalId) => _context.Terminals.Where(t => t.Id == terminalId).Select(t => t.ProvidedPort_Id).FirstOrDefault();
        public int GetTariffPlanIdByClientId(int clientId) => _context.Contracts.Where(c => c.Client_Id == clientId).Select(c => c.TariffPlan_ID).FirstOrDefault();

        public IPort GetPortByClientId(int clientId)
        {

            var terminalId = GetTerminalIdByClientId(clientId);

            return _mapper.Map<PortEntity, Port>(_context.Ports.Where(p => p.Id == GetPortIdByTerminalId(terminalId)).FirstOrDefault());
        }

        public ITerminal GetTerminalByClientId(int clientId) => _mapper.Map<TerminalEntity, Terminal>(_context.Terminals.Where(t => t.Id == GetTerminalIdByClientId(clientId)).FirstOrDefault());

        public ITariffPlan GetTariffPlanByClientId(int clientId)
        {
            var tariffPlanId = GetTariffPlanIdByClientId(clientId);

            return _mapper.Map<SecondMinuteTariffPlanEntity, SecondMinuteTariffPlan>(_context.TariffPlans.Where(t => t.Id == tariffPlanId).FirstOrDefault());
        }

    }
}
