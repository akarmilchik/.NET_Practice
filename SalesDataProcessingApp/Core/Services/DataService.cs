using Core.Interfaces;
using DAL;
using DAL.ModelsEntities;
using DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DataService : IDataService
    {
        private readonly Serilog.Core.Logger _logger;

        public ParseService _parseService;

        public DataRepository<OrderEntity> _orderRepo;

        public DataRepository<ClientEntity> _clientRepo;

        public DataRepository<ProductEntity> _productRepo;

        public DataService(DataContext context, Serilog.Core.Logger logger)
        {
            _parseService = new ParseService();

            _logger = logger;

            _orderRepo = new DataRepository<OrderEntity>(context);

            _clientRepo = new DataRepository<ClientEntity>(context);

            _productRepo = new DataRepository<ProductEntity>(context);
        }

        public void ProcessFile(object filePath)
        {
            var orders = _parseService.ReadCSVFile((string)filePath, _logger);

            SplitClientNames(ref orders);

            foreach (var order in orders)
            {
                ProcessOrderEntity(order);
            }
        }

        public void ProcessOrderEntity(OrderEntity orderEntity)
        {
            var existingClient = _clientRepo.Get(c => c.FirstName == orderEntity.Client.FirstName && c.LastName == orderEntity.Client.LastName).FirstOrDefault();

            var exisitingProduct = _productRepo.Get(o => o.Name == orderEntity.Product.Name && o.Cost == orderEntity.Product.Cost).FirstOrDefault();

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

            _orderRepo.Add(orderEntity);
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
