using DAL.ModelsEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class DataSeeder
    {
        private readonly DataContext _context;

        public DataSeeder(DataContext context)
        {
            _context = context;
        }

        private static readonly List<ClientEntity> Clients = new List<ClientEntity>
        {
            new ClientEntity { FirstName = "Alex", LastName = "Karm" },
            new ClientEntity { FirstName = "Donald", LastName = "Macdonald" },
            new ClientEntity { FirstName = "Elvis", LastName = "Presley" },
            new ClientEntity { FirstName = "Marty", LastName = "Kalkin" }
        };

        private static readonly List<ProductEntity> Products = new List<ProductEntity>
        {
            new ProductEntity { Name = "Lavender Bath Salt", Cost = 12.42m },
            new ProductEntity { Name = "Snowboard Cold Weather Gloves", Cost = 18.69m },
            new ProductEntity { Name = "Nighthawk Wi-Fi Router, R6700 Wireless", Cost = 57.99m },
            new ProductEntity { Name = "Samsung Galaxy Watch Active", Cost = 149.99m }
        };

        private static readonly List<OrderEntity> Orders = new List<OrderEntity>
        {
            new OrderEntity { Client = Clients[0], Product = Products[0], Date = new DateTime(2021, 01, 05) },
            new OrderEntity { Client = Clients[2], Product = Products[2], Date = new DateTime(2020, 12, 14) }
        };

        public void SeedData()
        {
            if (_context.Database.Exists())
            {
                if (!_context.Clients.Any())
                {
                    _context.Clients.AddRange(Clients);
                }

                if (!_context.Products.Any())
                {
                    _context.Products.AddRange(Products);
                }

                if (!_context.Orders.Any())
                {
                    _context.Orders.AddRange(Orders);
                }               

                _context.SaveChanges();
            }
        }
    }
}
