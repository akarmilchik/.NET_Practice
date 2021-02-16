using Microsoft.EntityFrameworkCore;
using SalesStatistics.Core.Queries;
using SalesStatistics.DAL;
using SalesStatistics.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesStatistics.Core.Services
{
    public class ProductsService : IProductsService
    {
        private readonly DataContext context;
        private readonly ISortingProvider<Product> sortingProvider;

        public ProductsService(DataContext context, ISortingProvider<Product> sortingProvider)
        {
            this.context = context;
            this.sortingProvider = sortingProvider;
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

        public async Task<PagedResult<Product>> GetProducts(BaseQuery query)
        {
            var queryable = context.Products.AsQueryable();

            var count = await queryable.CountAsync();

            queryable = sortingProvider.ApplySorting(queryable, query);
            queryable = queryable.ApplyPagination(query);

            var items = await queryable.ToListAsync();

            return new PagedResult<Product> { TotalCount = count, Items = items };
        }

        public IQueryable<Product> GetProductsFilteredByPrice(decimal cost)
        {
            var products = context.Products.Where(p => p.Cost > cost);

            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await context.Products.FindAsync(id);
        }
    }
}
