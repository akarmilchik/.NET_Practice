using SalesStatistics.DAL.Models;
using System.Collections.Generic;

namespace SalesStatistics.Models
{
    public class OrdersViewModel
    {
        public ICollection<Order> Orders { get; set; }
    }
}
