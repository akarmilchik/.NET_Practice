using ATS.Core.Interfaces;
using ATS.DAL;
using ATS.DAL.Models.Billing;

namespace ATS.Core.Services
{
    class DataService : IDataService
    {
        private readonly DataContext _context;

        public DataService(DataContext context)
        {
            _context = context;
        }

        public void AddContractToDb(Contract contract)
        {
            _context.Database.EnsureCreated();

            _context.Contracts.Add(contract);

            _context.SaveChanges();
        }

        public void RemoveContractToDb(Contract contract)
        {
            _context.Database.EnsureCreated();

            _context.Contracts.Remove(contract);

            _context.SaveChanges();
        }
    }
}
