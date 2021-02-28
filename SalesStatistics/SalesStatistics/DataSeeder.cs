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
        private readonly DataContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;


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

        private static readonly List<Country> Countries = new List<Country>
        {
            new Country { Name = "USA" },
            new Country { Name = "Russua" },
            new Country { Name = "Belarus" },
            new Country { Name = "China" }
        };

        private static readonly List<Client> Clients = new List<Client>
        {
            new Client { FirstName = "Alex", LastName = "Karm", Age = 24, Country = Countries[2] },
            new Client { FirstName = "Donald", LastName = "Macdonald", Age = 38, Country = Countries[0] },
            new Client { FirstName = "Elvis", LastName = "Presley", Age = 46, Country = Countries[1] },
            new Client { FirstName = "Marty", LastName = "Kalkin", Age = 51, Country = Countries[3] },
            new Client { FirstName = "Alina", LastName = "Barinova", Age = 22, Country = Countries[1] },
            new Client { FirstName = "Scarlett", LastName = "Johansson", Age = 38, Country = Countries[0] },
            new Client { FirstName = "Anastasiya", LastName = "Kamenskikh", Age = 51, Country = Countries[2] },
            new Client { FirstName = "Sergey", LastName = "Rudolff", Age = 51, Country = Countries[3] }
        };

        private static readonly List<Product> Products = new List<Product>
        {
            new Product { Name = "Lavender Bath Salt", Cost = 12.42m, Weight = 120 },
            new Product { Name = "Snowboard Cold Weather Gloves", Cost = 18.69m, Weight = 50 },
            new Product { Name = "Nighthawk WiFi Route R6700 Wireless", Cost = 57.99m, Weight = 350 },
            new Product { Name = "Samsung Galaxy Watch Active", Cost = 149.99m, Weight = 45 },
            new Product { Name = "Fashion shoes el Camino", Cost = 299.99m, Weight = 300 },
            new Product { Name = "Amazon Kit", Cost = 500.00m, Weight = 450 },
            new Product { Name = "Lavender Bath Salt", Cost = 18.00m, Weight = 120 },
            new Product { Name = "Samsung Galaxy Watch Active", Cost = 99.50m, Weight = 45 },
            new Product { Name = "Programs kit", Cost = 112.99m, Weight = 560 },
            new Product { Name = "CocaCola Zero 1l", Cost = 2.28m, Weight = 1000 },
            new Product { Name = "Green Apples", Cost = 1.30m, Weight = 300 },
            new Product { Name = "Dogs eat 100g", Cost = 0.72m, Weight = 100 },
            new Product { Name = "Solar Guitar", Cost = 76.45m, Weight = 2600 },
            new Product { Name = "East Sushi set", Cost = 45.10m, Weight = 700 },
            new Product { Name = "Smart Microwave Oven", Cost = 320.00m, Weight = 6500 },
            new Product { Name = "AI209 Waterproof pH Tester Kit", Cost = 77.50m, Weight = 350 },
            new Product { Name = "Lamborgini Aventador Kids Toy", Cost = 45m, Weight = 130 },
            new Product { Name = "Ecosmart Fleece Hooded Sweatshirt", Cost = 32.05m, Weight = 620 },
            new Product { Name = "Stainless Steel Office Scissors", Cost = 11.50m, Weight = 180 }
        };

        private static readonly List<Order> Orders = new List<Order>
        {
            new Order { Client = Clients[0], Product = Products[18], Date = new DateTime(2020, 02, 12) },
            new Order { Client = Clients[1], Product = Products[17], Date = new DateTime(2020, 02, 18) },
            new Order { Client = Clients[2], Product = Products[16], Date = new DateTime(2021, 03, 31) },
            new Order { Client = Clients[3], Product = Products[15], Date = new DateTime(2020, 04, 17) },
            new Order { Client = Clients[4], Product = Products[14], Date = new DateTime(2020, 04, 21) },
            new Order { Client = Clients[5], Product = Products[13], Date = new DateTime(2020, 04, 23) },
            new Order { Client = Clients[6], Product = Products[12], Date = new DateTime(2020, 04, 24) },
            new Order { Client = Clients[7], Product = Products[11], Date = new DateTime(2020, 05, 08) },
            new Order { Client = Clients[6], Product = Products[10], Date = new DateTime(2020, 05, 20) },
            new Order { Client = Clients[5], Product = Products[9], Date = new DateTime(2020, 05, 21) },
            new Order { Client = Clients[4], Product = Products[8], Date = new DateTime(2020, 06, 08) },
            new Order { Client = Clients[3], Product = Products[7], Date = new DateTime(2020, 07, 02) },
            new Order { Client = Clients[2], Product = Products[6], Date = new DateTime(2020, 07, 13) },
            new Order { Client = Clients[1], Product = Products[5], Date = new DateTime(2020, 09, 03) },
            new Order { Client = Clients[0], Product = Products[4], Date = new DateTime(2020, 11, 06) },
            new Order { Client = Clients[1], Product = Products[3], Date = new DateTime(2020, 11, 11) },
            new Order { Client = Clients[2], Product = Products[2], Date = new DateTime(2020, 11, 16) },
            new Order { Client = Clients[3], Product = Products[1], Date = new DateTime(2020, 11, 26) },
            new Order { Client = Clients[4], Product = Products[0], Date = new DateTime(2020, 12, 02) },
            new Order { Client = Clients[5], Product = Products[5], Date = new DateTime(2020, 12, 11) },
            new Order { Client = Clients[6], Product = Products[14], Date = new DateTime(2021, 01, 29) },
            new Order { Client = Clients[6], Product = Products[18], Date = new DateTime(2020, 02, 12) },
            new Order { Client = Clients[5], Product = Products[17], Date = new DateTime(2020, 02, 18) },
            new Order { Client = Clients[4], Product = Products[16], Date = new DateTime(2021, 03, 31) },
            new Order { Client = Clients[3], Product = Products[15], Date = new DateTime(2020, 04, 17) },
            new Order { Client = Clients[2], Product = Products[14], Date = new DateTime(2020, 04, 21) },
            new Order { Client = Clients[1], Product = Products[13], Date = new DateTime(2020, 07, 23) },
            new Order { Client = Clients[0], Product = Products[12], Date = new DateTime(2020, 04, 24) },
            new Order { Client = Clients[7], Product = Products[11], Date = new DateTime(2020, 05, 08) },
            new Order { Client = Clients[1], Product = Products[10], Date = new DateTime(2020, 09, 20) },
            new Order { Client = Clients[3], Product = Products[9], Date = new DateTime(2020, 05, 21) },
            new Order { Client = Clients[4], Product = Products[12], Date = new DateTime(2020, 06, 08) },
            new Order { Client = Clients[5], Product = Products[7], Date = new DateTime(2020, 07, 02) },
            new Order { Client = Clients[6], Product = Products[6], Date = new DateTime(2020, 07, 13) },
            new Order { Client = Clients[7], Product = Products[5], Date = new DateTime(2020, 09, 03) },
            new Order { Client = Clients[6], Product = Products[16], Date = new DateTime(2020, 11, 06) },
            new Order { Client = Clients[5], Product = Products[3], Date = new DateTime(2020, 11, 11) },
            new Order { Client = Clients[4], Product = Products[2], Date = new DateTime(2020, 03, 16) },
            new Order { Client = Clients[3], Product = Products[9], Date = new DateTime(2020, 11, 26) },
            new Order { Client = Clients[2], Product = Products[0], Date = new DateTime(2020, 12, 02) },
            new Order { Client = Clients[1], Product = Products[5], Date = new DateTime(2020, 05, 11) },
            new Order { Client = Clients[0], Product = Products[14], Date = new DateTime(2021, 01, 29) }
        };

        public DataSeeder(DataContext context, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDataAsync()
        {
            if (await _context.Database.CanConnectAsync())
            {
                foreach (IdentityRole role in Roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role.Name))
                    {
                        await _roleManager.CreateAsync(role);
                    }
                }

                await _context.SaveChangesAsync();

                if (await _userManager.FindByNameAsync(Users[0].UserName) == null)
                {
                    IdentityResult result = _userManager.CreateAsync(Users[0], "admin111").Result;

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(Users[0], Roles[0].Name);
                    }

                }

                if (await _userManager.FindByNameAsync(Users[1].UserName) == null)
                {
                    IdentityResult result = _userManager.CreateAsync(Users[1], "user222").Result;

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(Users[1], Roles[1].Name);
                    }
                }

                if (await _userManager.FindByNameAsync(Users[2].UserName) == null)
                {
                    IdentityResult result = _userManager.CreateAsync(Users[2], "user333").Result;


                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(Users[2], Roles[1].Name);
                    }
                }

                await _context.SaveChangesAsync();

                if (!_context.Clients.Any())
                {
                    await _context.Clients.AddRangeAsync(Clients);
                }

                if (!_context.Products.Any())
                {
                    await _context.Products.AddRangeAsync(Products);
                }

                if (!_context.Orders.Any())
                {
                    await _context.Orders.AddRangeAsync(Orders);
                }               

                await _context.SaveChangesAsync();
            }
        }
    }
}
