using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesStatistics.Controllers.Api.Models;
using SalesStatistics.Core.Queries;
using SalesStatistics.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesStatistics.Controllers.Api
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientsController : Controller
    {
        private readonly IClientsService _clientsService;
        private readonly IMapper _mapper;

        public ClientsController(IClientsService clientsService, IMapper mapper)
        {
            _clientsService = clientsService;
            _mapper = mapper;
        }

        //GET
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClients([FromQuery] ClientQuery query)
        {
            var pagedResult = await _clientsService.GetClientsQuery(query);

            return Ok(_mapper.Map<IEnumerable<ClientResource>>(pagedResult));
        }

        //GET
        [HttpGet]
        [Route("countries")]
        [ProducesResponseType(typeof(IEnumerable<CountryResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountries()
        {
            var pagedResult = await _clientsService.GetCountries();

            return Ok(_mapper.Map<IEnumerable<CountryResource>>(pagedResult));
        }
    }
}
