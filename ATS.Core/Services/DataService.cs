using ATS.Core.Interfaces;
using ATS.DAL;
using ATS.DAL.Models.Billing;
using ATS.DAL.ModelsEntities.Billing;
using System.Collections.Generic;
using System.Linq;

namespace ATS.Core.Services
{
    internal class DataService : IDataService
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
            _context.Database.EnsureCreated();

            _context.Contracts.Add(contract);

            _context.SaveChanges();
        }

        public void RemoveContractToDb(ContractEntity contract)
        {
            _context.Database.EnsureCreated();

            _context.Contracts.Remove(contract);

            _context.SaveChanges();
        }


        public List<Client> GetClients()
        {
            List<Client> result = new List<Client>();

            foreach (var clientEntity in _context.Clients.ToList())
            {
                result.Add(_mapper.Map<ClientEntity, Client>(clientEntity));
            }

            return result;
        }

        public List<ContractToTariffPlanBinding> GetContractsBindings()
        {
            List<ContractToTariffPlanBinding> result = new List<ContractToTariffPlanBinding>();

            foreach (var contractsBinding in _context.ContractToTariffPlanBindings.ToList())
            {
                result.Add(_mapper.Map<ContractToTariffPlanBindingEntity, ContractToTariffPlanBinding>(contractsBinding));
            }

            return result;
        }

        public List<Contract> GetContracts()
        {
            List<Contract> result = new List<Contract>();

            foreach (var contractEntity in _context.Contracts.ToList())
            {
                result.Add(_mapper.Map<ContractEntity, Contract>(contractEntity));
            }

            return result;
        }
    }
}