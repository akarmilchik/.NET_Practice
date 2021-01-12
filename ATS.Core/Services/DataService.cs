using ATS.Core.Interfaces;
using ATS.DAL;
using ATS.DAL.ModelsEntities.Billing;

namespace ATS.Core.Services
{
    internal class DataService : IDataService
    {
        private readonly DataContext _context;

        public DataService(DataContext context)
        {
            _context = context;
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
    }
}