using Microsoft.AspNetCore.Mvc;
using SalesStatistics.Core.Services;
using SalesStatistics.Models;
using System.Threading.Tasks;

namespace SalesStatistics.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;

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
