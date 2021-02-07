using Core.Interfaces;
using DAL.Interfaces;
using DAL.ModelsEntities;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DataService : IDataService
    {
        private readonly IParseService _parseService;
        private IRepository _repository;
        private ILogger _logger;

        public DataService(IParseService parseService, IRepository repository, ILogger logger)
        {
            _parseService = parseService;
            _repository = repository;
            _logger = logger;
        }

        public void ProcessFile(object filePath)
        {
            _logger.Information($"Task id:{Task.CurrentId};  File:{filePath}");

            var orders = _parseService.ReadCSVFile((string)filePath);

            SplitClientNames(ref orders);

            foreach (var order in orders)
            {
                ProcessOrderEntity(order);
            }

        }

        public void ProcessOrderEntity(OrderEntity orderEntity)
        {
            var existingClient = _repository.Get<ClientEntity>(c => c.FirstName == orderEntity.Client.FirstName && c.LastName == orderEntity.Client.LastName);

            var exisitingProduct = _repository.Get<ProductEntity>(o => o.Name == orderEntity.Product.Name && o.Cost == orderEntity.Product.Cost);

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

            _repository.Add(orderEntity);
        }

        private void SplitClientNames(ref List<OrderEntity> orders)
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