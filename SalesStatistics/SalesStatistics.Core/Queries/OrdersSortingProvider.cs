using SalesStatistics.DAL.Models;
using System;
using System.Linq.Expressions;

namespace SalesStatistics.Core.Queries
{
    public class OrdersSortingProvider : BaseSortingProvider<Order>
    {
        protected override Expression<Func<Order, object>> GetSortExpression(BaseQuery query)
        {
            return query.SortBy switch
            {
                "Client" => e => e.Client,
                "Product" => e => e.Product,
                _ => e => e.Id,

            };
        }
    }
}
