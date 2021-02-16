using SalesStatistics.Core.Queries;
using SalesStatistics.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesStatistics.Core.Services
{
    public interface IOrdersService
    {
        Task<IEnumerable<Client>> GetClients();
        Task<Product> GetOrderById(int id);
        Task<PagedResult<Order>> GetOrders(OrderQuery query);
        IQueryable<Order> GetOrdersFilteredByProduct(int productId);
        Task<IEnumerable<Order>> GetOrdersWithProducts();
    }
}
