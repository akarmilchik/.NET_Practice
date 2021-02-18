using SalesStatistics.Core.Queries;
using SalesStatistics.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesStatistics.Core.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Client>> GetClients();
        Task<IEnumerable<Order>> GetOrdersWithProducts();
        Task<Product> GetProductById(int id);
        Task<PagedResult<Product>> GetProducts(BaseQuery query);
        IQueryable<Product> GetProductsFilteredByPrice(decimal cost);
    }
}
