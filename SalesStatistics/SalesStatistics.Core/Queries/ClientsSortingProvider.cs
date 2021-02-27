using SalesStatistics.DAL.Models;
using System;
using System.Linq.Expressions;

namespace SalesStatistics.Core.Queries
{
    public class ClientsSortingProvider : BaseSortingProvider<Client>
    {
        protected override Expression<Func<Client, object>> GetSortExpression(BaseQuery query)
        {
            return query.SortBy switch
            {
                "Country" => c => c.Country,
                "Age" => c => c.Age,
                _ => c => c.Id,

            };
        }
    }
}
