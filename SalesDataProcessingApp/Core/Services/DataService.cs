using Core.Interfaces;
using DAL;
using DAL.Interfaces;
using DAL.ModelsEntities;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class DataService : IDataService
    {
        private readonly IParseService _parseService;
        private readonly IRepository _repository;
        private readonly DataContext _context;

        public DataService(DataContext context, IParseService parseService, IRepository repository)
        {
            _parseService = parseService;
            _context = context;
            _repository = repository; 
        }

        public void ProcessFile(object filePath)
        {
            var orders = _parseService.ReadCSVFile((string)filePath);

            SplitClientNames(ref orders);

            foreach (var order in orders)
            {
                ProcessOrderEntity(order);
            }

            _context.Dispose();
        }

        public void ProcessOrderEntity(OrderEntity orderEntity)
        {
            var existingClient = _repository.Get<ClientEntity>(c => c.FirstName == orderEntity.Client.FirstName && c.LastName == orderEntity.Client.LastName).FirstOrDefault();

            var exisitingProduct = _repository.Get<ProductEntity>(o => o.Name == orderEntity.Product.Name && o.Cost == orderEntity.Product.Cost).FirstOrDefault();

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