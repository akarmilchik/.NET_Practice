using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SalesStatistics.Core.Services;
using SalesStatistics.DAL.Models;
using SalesStatistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesStatistics.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IClientsService _clientsService;
        private readonly IProductsService _productsService;
        private readonly IStringLocalizer<OrdersController> _localizer;

        public OrdersController(IOrdersService ordersService, IClientsService clientsService, IProductsService productsService, IStringLocalizer<OrdersController> localizer)
        {
            _ordersService = ordersService;
            _localizer = localizer;
            _clientsService = clientsService;
            _productsService = productsService;

        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = _localizer["ordersTitle"];

            var orders = await _ordersService.GetOrders();
            orders = orders.GroupBy(o => o.Product.Name).Select(g => g.First()).ToList();
 
            var model = new OrdersViewModel
            {
                Clients = await _clientsService.GetClients(),
                Products = await _productsService.GetProducts(),
                ChartModel = new ChartViewModel { 
                    Lables = orders.Select(o => o.Product.Name).Distinct(),
                    Values = orders.Select(o => Convert.ToInt32(o.Product.Cost))
                }
            };

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateOrder()
        {
            ViewData["Title"] = _localizer["createOrder"];

            var clients = await _clientsService.GetClients();
            var products = await _productsService.GetProducts();

            ViewBag.Clients = new SelectList(clients, "Id", "LastName"); 
            ViewBag.Products = new SelectList(products, "Id", "Name"); 

            return View("CreateOrder");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateOrder(OrderCreateEditModel model)
        {
            Order order = new Order() 
            {
                ClientId = model.Client.Id, 
                Client = await _clientsService.GetClientById(model.Client.Id),
                ProductId = model.Product.Id, 
                Product = await _productsService.GetProductById(model.Product.Id),
                Date = model.Date
            };

            await _ordersService.AddOrderToDb(order);

            return View("Index");
        }

    }
}
