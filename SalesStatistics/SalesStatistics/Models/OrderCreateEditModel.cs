using Microsoft.AspNetCore.Mvc.Rendering;
using SalesStatistics.DAL.Models;
using System;

namespace SalesStatistics.Models
{
    public class OrderCreateEditModel
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Product Product { get; set; }
        public SelectList Clients { get; set; }
        public SelectList Products { get; set; }
    }
}
