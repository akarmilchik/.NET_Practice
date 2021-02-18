using System;

namespace SalesStatistics.Controllers.Api.Models
{
    public class OrderResource
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
    }
}
