using Core.Interfaces;
using DAL;
using DAL.ModelsEntities;
using DAL.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_2.Services
{
    public class DataService : IDataService
    {
        public ParseService _parseService;

        public DataRepository<OrderEntity> _orderRepo;
        public DataRepository<ClientEntity> _clientRepo;
        public DataRepository<ProductEntity> _productRepo;

        public DataService(DataContext context)
        {
            _parseService = new ParseService();

            _orderRepo = new DataRepository<OrderEntity>(context);
            _clientRepo = new DataRepository<ClientEntity>(context);
            _productRepo = new DataRepository<ProductEntity>(context);
        }

        public async Task ProcessFile(object filePath)
        {
            var orders = _parseService.ReadCSVFile((string)filePath);

            orders = SplitClientNames(orders);

            foreach (var order in orders)
            {
                var existingClient = _clientRepo.Get(c => c.FirstName == order.Client.FirstName && c.LastName == order.Client.LastName).FirstOrDefault();

                var exisitingProduct = _productRepo.Get(o => o.Name == order.Product.Name && o.Cost == order.Product.Cost).FirstOrDefault();

                if (existingClient != null)
                {
                    order.ClientId = existingClient.Id;

                    order.Client = null;
                }

                if (exisitingProduct != null)
                {
                    order.ProductId = exisitingProduct.Id;

                    order.Product = null;
                }

                await _orderRepo.Add(order);
            }
        }

        public List<OrderEntity> SplitClientNames(List<OrderEntity> orders)
        {
            foreach (var order in orders)
            {
                var splittedName = order.Client.FirstName.Split(' ');

                order.Client.FirstName = splittedName[0];

                order.Client.LastName = splittedName[1];
            }

            return orders;
        }
    }
}
