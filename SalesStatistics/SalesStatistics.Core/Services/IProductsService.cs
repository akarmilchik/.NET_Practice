using SalesStatistics.Core.Queries;
using SalesStatistics.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesStatistics.Core.Services
{
    public interface IProductsService
    {
        Task<List<Product>> GetProducts();
        Task<PagedResult<Product>> GetProductsQuery(ProductQuery query);
        IQueryable<Product> GetProductsFilteredByPrice(decimal cost);
        Task<Product> GetProductById(int id);

    }
}
