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

        public ProductsService(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<PagedResult<Product>> GetProductsQuery(ProductQuery query)
        {
            var queryable = context.Products.AsQueryable();

            if (query.Weights != "")
            {
                var weights = FormatParameter(query.Weights);

                if (weights != null)
                {
                    queryable = queryable.Where(v => v.Weight >= weights[0] && v.Weight <= weights[1]);
                }
            }
            if (query.Costs != "")
            {
                var costs = FormatParameter(query.Costs);

                if (costs != null)
                {
                    queryable = queryable.Where(v => v.Cost >= costs[0] && v.Cost <= costs[1]);
                }
            }


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

        private List<int> FormatParameter(string parameter)
        {
            if (parameter == null)
            {
                return null;
            }

            return parameter.Split('-').Select(int.Parse).ToList();
        }
    }
}
