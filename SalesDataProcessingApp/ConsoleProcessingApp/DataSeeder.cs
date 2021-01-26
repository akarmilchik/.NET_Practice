using DAL;
using DAL.ModelsEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleProcessingApp
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
            new ClientEntity { FirstName = "Jeff", LastName = "Bezos" },
            new ClientEntity { FirstName = "Elvis", LastName = "Presley" },
            new ClientEntity { FirstName = "Marty", LastName = "McFly" },
            new ClientEntity { FirstName = "Scarlett", LastName = "Johansson" }
        };

        private static readonly List<ProductEntity> Products = new List<ProductEntity>
        {
            new ProductEntity { Name = "Lavender Scented Premium Epsom Bath Salt", Cost = 12.42m },
            new ProductEntity { Name = "Thinsulate Snowboard Snowmobile Cold Weather Gloves", Cost = 18.69m },
            new ProductEntity { Name = "Nighthawk Smart Wi-Fi Router, R6700 - AC1750 Wireless", Cost = 57.99m },
            new ProductEntity { Name = "Samsung Galaxy Watch Active (40MM, GPS, Bluetooth)", Cost = 149.99m },
            new ProductEntity { Name = "Smart Oven, a Certified for Humans device – plus Echo Dot", Cost = 249.99m },
            new ProductEntity { Name = "Stainless Steel Office Scissors - Pack of 3", Cost = 11.50m },
            new ProductEntity { Name = "Seagate 2TB External Hard Drive HDD – USB 3.0 for PC, Mac, PS4, & Xbox", Cost = 58.99m },
            new ProductEntity { Name = "Fossil Men's Grant Stainless Steel Chronograph Quartz Watch", Cost = 96.75m },
            new ProductEntity { Name = "Ecosmart Fleece Hooded Sweatshirt", Cost = 12.00m },
            new ProductEntity { Name = "Apera Instruments AI209 Value Series PH20 Waterproof pH Tester Kit", Cost = 49.75m }
        };

        private static readonly List<OrderEntity> Orders = new List<OrderEntity>
        {
            new OrderEntity { Client = Clients[0], Product = Products[0], Date = new DateTime(2021, 01, 05) },
            new OrderEntity { Client = Clients[2], Product = Products[5], Date = new DateTime(2020, 12, 14) },
            new OrderEntity { Client = Clients[4], Product = Products[1], Date = new DateTime(2021, 01, 17) },
            new OrderEntity { Client = Clients[2], Product = Products[7], Date = new DateTime(2020, 10, 27) }
        };

        public void SeedData()
        {
            if (_context.Database.CanConnect())
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
