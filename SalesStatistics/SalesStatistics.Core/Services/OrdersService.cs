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

        public async Task<List<Order>> GetOrders()
        {
            return await context.Orders.Include(o => o.Client).Include(o => o.Product).ToListAsync();
        }

        public async Task<List<Order>> GetOrdersWithProducts()
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

        public async Task<PagedResult<Order>> GetOrdersQuery(OrderQuery query)
        {
            var queryable = context.Orders.Include(o => o.Client).Include(o => o.Product).AsQueryable();

            if (query.Products != null)
            {
                queryable = queryable.Where(v => query.Products.Contains(v.ProductId));
            }
            if (query.Clients != null)
            {
                queryable = queryable.Where(v => query.Clients.Contains(v.ClientId));
            }

            if (query.DateFrom != null)
            {
                queryable = queryable.Where(e => e.Date >= query.DateFrom);
            }

            if (query.DateTo != null)
            {
                queryable = queryable.Where(e => e.Date <= query.DateTo);
            }
            var count = await queryable.CountAsync();

            queryable = sortingProvider.ApplySorting(queryable, query);

            queryable = queryable.ApplyPagination(query);

            var items = await queryable.ToListAsync();

            return new PagedResult<Order> { TotalCount = count, Items = items };
        }

        public async Task<IEnumerable<string>> GetMatchedOrders(string q, int countOfRelevantResults)
        {
            var queryable = context.Orders.AsQueryable();

            var matchedOrders = queryable
                .Where(e => e.Product.Name.Contains(q.Trim().ToLower()))
                .OrderBy(e => e.Product.Name)
                .Select(e => e.Product.Name)
                .Take(countOfRelevantResults);

            return await matchedOrders.ToListAsync();
        }
    }
}
