using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SalesStatistics.Core.Services;
using SalesStatistics.Models;
using System.Threading.Tasks;

namespace SalesStatistics.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IStringLocalizer<OrdersController> localizer;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Orders";

            var orders = await _ordersService.GetOrders();

            var model = new OrdersViewModel
            {
                Orders = orders
            };

            return View(model);
        }
    }
}
