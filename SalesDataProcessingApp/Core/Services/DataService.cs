using Core.Interfaces;
using DAL;
using DAL.ModelsEntities;
using DAL.Repository;

namespace Core.Services
{
    public class DataService : IDataService
    {
        private readonly DataContext _context;

        public ParseService _parseService;


        public DataRepository<OrderEntity> _orderRepo;

        public DataService(DataContext context)
        {
            _context = context;

            _parseService = new ParseService();

            _orderRepo = new DataRepository<OrderEntity>(context);
        }

        public void ProcessFiles(string filePath)
        {
            var orders = _parseService.ReadCSVFile(filePath);

            foreach (var order in orders)
            {
                _orderRepo.Add(order);
            }
        }
    }
}
