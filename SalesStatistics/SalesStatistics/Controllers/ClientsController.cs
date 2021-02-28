using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SalesStatistics.Core.Services;
using SalesStatistics.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SalesStatistics.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientsService _clientsService;
        private readonly IStringLocalizer<ClientsController> _localizer;

        public ClientsController(IClientsService clientsService, IStringLocalizer<ClientsController> localizer)
        {
            _clientsService = clientsService;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = _localizer["Clients"];

            var clients = await _clientsService.GetClients();

            var model = new ClientsViewModel
            {
                Countries = await _clientsService.GetCountries(),
                ChartModel = new ChartViewModel
                {
                    Lables = clients.Select(c => c.FirstName + " " + c.LastName),
                    Values = clients.Select(c => c.Age)
                }
            };

            return View(model);
        }
    }
}
