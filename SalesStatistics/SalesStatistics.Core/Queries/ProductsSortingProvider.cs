using SalesStatistics.DAL.Models;
using System;
using System.Linq.Expressions;

namespace SalesStatistics.Core.Queries
{
    class ProductsSortingProvider : BaseSortingProvider<Product>
    {
        protected override Expression<Func<Product, object>> GetSortExpression(BaseQuery query)
        {
            return query.SortBy switch
            {
                "Weight" => p => p.Weight,
                "Cost" => p => p.Cost,
                _ => p => p.Id,

            };
        }
    }
}
