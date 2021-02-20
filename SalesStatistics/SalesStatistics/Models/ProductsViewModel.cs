using SalesStatistics.DAL.Models;
using System.Collections.Generic;

namespace SalesStatistics.Models
{
    public class ProductsViewModel
    {
        public ICollection<Product> Products { get; set; }
    }
}
