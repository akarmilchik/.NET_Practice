using Microsoft.AspNetCore.Mvc;
using SalesStatistics.Core.Services;
using SalesStatistics.Models;
using System.Threading.Tasks;

namespace SalesStatistics.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientsService _clientsService;

        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Clients";

            var model = new ClientsViewModel
            {
                Countries = await _clientsService.GetCountries()
            };

            return View(model);
        }
    }
}
