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
        private readonly DataContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;


        private static readonly List<User> Users = new List<User>
        {
            new User { FirstName = "Alexey", LastName = "Karm", UserName = "alexey.karm@mail.ru", Email = "alexey.karm@mail.ru" },
            new User { FirstName = "Jominez", LastName = "Maxwell", UserName = "j@mail.ru", Email = "j@mail.ru" },
            new User { FirstName = "Alibaba", LastName = "Bestseller", UserName = "a@mail.ru", Email = "a@mail.ru" }
        };

        private static readonly List<IdentityRole> Roles = new List<IdentityRole>
        {
            new IdentityRole { Name = "Administrator" },
            new IdentityRole { Name = "User" }
        };

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

        public DataSeeder(DataContext context, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedDataAsync()
        {
            if (await context.Database.CanConnectAsync())
            {
                // Roles
                foreach (IdentityRole role in Roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

                await context.SaveChangesAsync();

                // Create Users and sync with Roles
                if (await userManager.FindByNameAsync(Users[0].UserName) == null)
                {
                    IdentityResult result = userManager.CreateAsync(Users[0], "admin111").Result;

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(Users[0], Roles[0].Name);
                    }

                }

                if (await userManager.FindByNameAsync(Users[1].UserName) == null)
                {
                    IdentityResult result = userManager.CreateAsync(Users[1], "user222").Result;

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(Users[1], Roles[1].Name);
                    }
                }

                if (await userManager.FindByNameAsync(Users[2].UserName) == null)
                {
                    IdentityResult result = userManager.CreateAsync(Users[2], "user333").Result;


                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(Users[2], Roles[1].Name);
                    }
                }

                await context.SaveChangesAsync();

                if (!context.Clients.Any())
                {
                    await context.Clients.AddRangeAsync(Clients);
                }

                if (!context.Products.Any())
                {
                    await context.Products.AddRangeAsync(Products);
                }

                if (!context.Orders.Any())
                {
                    await context.Orders.AddRangeAsync(Orders);
                }               

                await context.SaveChangesAsync();
            }
        
    }
    }
}
