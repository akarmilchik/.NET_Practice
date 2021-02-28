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
        private readonly DataContext _context;
        private readonly ISortingProvider<Order> _sortingProvider;

        public OrdersService(DataContext context, ISortingProvider<Order> sortingProvider)
        {
            _context = context;
            _sortingProvider = sortingProvider;
        }

        public OrdersService(DataContext context)
        {
            _context = context;
        }

        public async Task AddOrderToDb(Order item)
        {
            _context.Database.EnsureCreated();

            await _context.Orders.AddAsync(item);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.Include(o => o.Client).Include(o => o.Product).ToListAsync();
        }

        public async Task<List<Order>> GetOrdersWithProducts()
        {
            var categories = _context.Orders.Include(o => o.Product);

            return await categories.ToListAsync();
        }

        public IQueryable<Order> GetOrdersFilteredByProduct(int productId)
        {
            var products = _context.Orders.Where(p => p.ProductId == productId);

            return products;
        }

        public async Task<Product> GetOrderById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<PagedResult<Order>> GetOrdersQuery(OrderQuery query)
        {
            var queryable = _context.Orders.Include(o => o.Client).Include(o => o.Product).AsQueryable();

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

            queryable = _sortingProvider.ApplySorting(queryable, query);

            queryable = queryable.ApplyPagination(query);

            var items = await queryable.ToListAsync();

            return new PagedResult<Order> { TotalCount = count, Items = items };
        }
    }
}
