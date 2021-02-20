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

            var clients = await _clientsService.GetClients();

            var model = new ClientsViewModel
            {
                Clients = clients
            };

            return View(model);
        }
    }
}
