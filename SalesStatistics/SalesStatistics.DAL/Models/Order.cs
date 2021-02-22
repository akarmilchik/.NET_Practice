using System;

namespace SalesStatistics.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Product Product { get; set; }
    }
}