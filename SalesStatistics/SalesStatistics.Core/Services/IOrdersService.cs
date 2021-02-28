using SalesStatistics.Core.Queries;
using SalesStatistics.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesStatistics.Core.Services
{
    public interface IOrdersService
    {
        Task AddOrderToDb(Order item);
        Task<Product> GetOrderById(int id);
        Task<List<Order>> GetOrders();
        IQueryable<Order> GetOrdersFilteredByProduct(int productId);
        Task<PagedResult<Order>> GetOrdersQuery(OrderQuery query);
        Task<List<Order>> GetOrdersWithProducts();
    }
}
