using Microsoft.EntityFrameworkCore;
using SalesStatistics.Core.Queries;
using SalesStatistics.DAL;
using SalesStatistics.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesStatistics.Core.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly DataContext context;
        private readonly ISortingProvider<Order> sortingProvider;

        public OrdersService(DataContext context, ISortingProvider<Order> sortingProvider)
        {
            this.context = context;
            this.sortingProvider = sortingProvider;
        }

        public OrdersService(DataContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            return await context.Clients.ToArrayAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersWithProducts()
        {
            var categories = context.Orders.Include(o => o.Product);

            return await categories.ToListAsync();
        }

        public async Task<PagedResult<Order>> GetOrders(OrderQuery query)
        {
            var queryable = context.Orders.AsQueryable();

            var count = await queryable.CountAsync();

            queryable = sortingProvider.ApplySorting(queryable, query);
            queryable = queryable.ApplyPagination(query);

            var items = await queryable.ToListAsync();

            return new PagedResult<Order> { TotalCount = count, Items = items };
        }

        public IQueryable<Order> GetOrdersFilteredByProduct(int productId)
        {
            var products = context.Orders.Where(p => p.ProductId == productId);

            return products;
        }

        public async Task<Product> GetOrderById(int id)
        {
            return await context.Products.FindAsync(id);
        }
    }
}
