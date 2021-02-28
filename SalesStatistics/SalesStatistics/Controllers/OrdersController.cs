using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SalesStatistics.Core.Services;
using SalesStatistics.DAL.Models;
using SalesStatistics.Models;
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

            var chartOrders = orders.Select(x => new object[] { x.Product.Name, x.Product.Cost }).ToArray();

            var model = new OrdersViewModel
            {
                Clients = await _clientsService.GetClients(),
                Products = await _productsService.GetProducts(),
                ChartOrders = chartOrders
            };

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> CreateOrder()
        {
            ViewData["Title"] = _localizer["createOrder"];

            var clients = await _clientsService.GetClients();
            var products = await _productsService.GetProducts();

            var model = new OrderCreateEditModel
            {
                Clients = new SelectList(clients, "Id", "FirstName LastName"),
                Products = new SelectList(products, "Id", "Name"),
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderCreateEditModel model)
        {
            var client = await _clientsService.GetClientById(model.Client.Id);
            var product = await _productsService.GetProductById(model.Product.Id);

            model.Client = client;
            model.Product = product;

            Order order = new Order() 
            {
                ClientId = model.Client.Id, 
                Client = model.Client, 
                ProductId = model.Product.Id, 
                Product = model.Product, 
                Date = model.Date
            };

            await _ordersService.AddOrderToDb(order);

            return View("Index");
        }

    }
}
