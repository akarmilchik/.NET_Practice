using SalesStatistics.Core.Constants;
using SalesStatistics.DAL.Models;
using System;
using System.Collections.Generic;

namespace SalesStatistics.Models
{
    public class OrdersViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        public ChartViewModel ChartModel { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public OrderSortParams SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
        public int PageSize { get; set; }
    }
}
