using Microsoft.AspNetCore.Mvc;
using SalesStatistics.Core.Services;
using SalesStatistics.Models;
using System.Threading.Tasks;

namespace SalesStatistics.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Products";

            var products = await _productsService.GetProducts();

            var model = new ProductsViewModel
            {
                Products = products
            };

            return View(model);
        }
    }
}
