using System.Linq;

namespace SalesStatistics.Core.Queries
{
    public interface ISortingProvider<T>
    {
        IOrderedQueryable<T> ApplySorting(IQueryable<T> queryable, BaseQuery query);
    }
}
