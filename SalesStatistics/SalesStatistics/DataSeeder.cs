using Microsoft.AspNetCore.Identity;
using SalesStatistics.DAL;
using SalesStatistics.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesStatistics
{
    public class DataSeeder
    {
        private static readonly List<Client> Clients = new List<Client>
        {
            new Client { FirstName = "Alex", LastName = "Karm" },
            new Client { FirstName = "Donald", LastName = "Macdonald" },
            new Client { FirstName = "Elvis", LastName = "Presley" },
            new Client { FirstName = "Marty", LastName = "Kalkin" }
        };

        private static readonly List<Product> Products = new List<Product>
        {
            new Product { Name = "Lavender Bath Salt", Cost = 12.42m },
            new Product { Name = "Snowboard Cold Weather Gloves", Cost = 18.69m },
            new Product { Name = "Nighthawk Wi-Fi Router, R6700 Wireless", Cost = 57.99m },
            new Product { Name = "Samsung Galaxy Watch Active", Cost = 149.99m }
        };

        private static readonly List<Order> Orders = new List<Order>
        {
            new Order { Client = Clients[0], Product = Products[0], Date = new DateTime(2021, 01, 05) },
            new Order { Client = Clients[2], Product = Products[2], Date = new DateTime(2020, 12, 14) }
        };

        private readonly DataContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public DataSeeder(DataContext context, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedDataAsync()
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
                await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });

            if (await userManager.FindByNameAsync("user1@example.com") == null)
            {
                var user = new User { UserName = "user1@example.com", Email = "user1@example.com" };
                await userManager.CreateAsync(user, "user123");
                await userManager.AddToRoleAsync(user, "Admin");
            }

            if (!context.Clients.Any()) await context.Clients.AddRangeAsync(Clients);

            if (!context.Products.Any()) await context.Products.AddRangeAsync(Products);

            if (!context.Orders.Any()) await context.Orders.AddRangeAsync(Orders);

            await context.SaveChangesAsync();
        }
    }
}
