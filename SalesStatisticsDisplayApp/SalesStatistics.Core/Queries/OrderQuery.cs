namespace SalesStatistics.Core.Queries
{
    public class OrderQuery : BaseQuery
    {
        public int[] Clients { get; set; }

        public int[] Products { get; set; }
    }
}
