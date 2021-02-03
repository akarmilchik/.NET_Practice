using Core.Interfaces;
using DAL;
using DAL.Interfaces;
using DAL.ModelsEntities;
using DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class DataService : IDataService
    {
        private readonly Serilog.Core.Logger _logger;

        private readonly ParseService _parseService;

        private readonly IGenericRepository _dataRepo;

        private readonly DataContext _context;

        public DataService(DataContext context, Serilog.Core.Logger logger)
        {
            _parseService = new ParseService();

            _logger = logger;

            _context = context;

            _dataRepo = new DataRepository(context); 
        }

        public void ProcessFile(object filePath)
        {
            var orders = _parseService.ReadCSVFile((string)filePath, _logger);

            SplitClientNames(ref orders);

            foreach (var order in orders)
            {
                ProcessOrderEntity(order);
            }

            _context.Dispose();
        }

        public void ProcessOrderEntity(OrderEntity orderEntity)
        {
            var existingClient = _dataRepo.Get<ClientEntity>(c => c.FirstName == orderEntity.Client.FirstName && c.LastName == orderEntity.Client.LastName).FirstOrDefault();

            var exisitingProduct = _dataRepo.Get<ProductEntity>(o => o.Name == orderEntity.Product.Name && o.Cost == orderEntity.Product.Cost).FirstOrDefault();

            if (existingClient != null)
            {
                orderEntity.ClientId = existingClient.Id;

                orderEntity.Client = null;
            }

            if (exisitingProduct != null)
            {
                orderEntity.ProductId = exisitingProduct.Id;

                orderEntity.Product = null;
            }

            _dataRepo.Add<OrderEntity>(orderEntity);
        }

        public void SplitClientNames(ref List<OrderEntity> orders)
        {
            foreach (var order in orders)
            {
                var splittedName = order.Client.FirstName.Split(' ');

                order.Client.FirstName = splittedName[0];

                order.Client.LastName = splittedName[1];
            }
        }
    }
}
