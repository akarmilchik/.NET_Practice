using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SalesStatistics.Core.Services;
using SalesStatistics.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SalesStatistics.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IStringLocalizer<ProductsController> _localizer;

        public ProductsController(IProductsService productsService, IStringLocalizer<ProductsController> localizer)
        {
            _productsService = productsService;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = _localizer["Products"];

            var products = await _productsService.GetProducts();

            products = products.GroupBy(o => o.Name).Select(g => g.First()).ToList();

            var model = new ProductsViewModel
            {
                Products = products,
                ChartModel = new ChartViewModel
                {
                    Lables = products.Select(c => c.Name),
                    Values = products.Select(c => Convert.ToInt32(c.Weight))
                }
            };

            return View(model);
        }
    }
}
