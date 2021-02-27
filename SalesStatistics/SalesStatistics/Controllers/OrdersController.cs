using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SalesStatistics.Core.Services;
using SalesStatistics.Models;
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

            var model = new OrdersViewModel
            {
                Clients = await _clientsService.GetClients(),
                Products = await _productsService.GetProducts()
            };

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> CreateOrder()
        {
            ViewData["Title"] = _localizer["createOrder"];

            var events = await _ordersService.GetOrders();

            var listEvents = new SelectList(events, "Id", "Name");

            ViewBag.Events = listEvents;

            return View("CreateOrder");
        }
    }
}
