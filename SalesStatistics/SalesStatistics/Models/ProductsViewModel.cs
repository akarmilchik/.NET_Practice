using SalesStatistics.Core.Constants;
using SalesStatistics.DAL.Models;
using System.Collections.Generic;

namespace SalesStatistics.Models
{
    public class ProductsViewModel
    {
        public ICollection<Product> Products { get; set; }
        public string Weights { get; set; }
        public string Costs { get; set; }
        public ChartViewModel ChartModel { get; set; }
        public ProductSortParams SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
        public int PageSize { get; set; }
    }
}
