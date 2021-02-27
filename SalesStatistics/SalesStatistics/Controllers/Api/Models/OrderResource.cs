using System;

namespace SalesStatistics.Controllers.Api.Models
{
    public class OrderResource
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
    }
}
