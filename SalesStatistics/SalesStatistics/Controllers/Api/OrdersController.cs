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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersService ordersService, IMapper mapper)
        {
            _ordersService = ordersService;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrdersByQuery([FromQuery] OrderQuery query)
        {
            var pagedResult = await _ordersService.GetOrdersQuery(query);

            var result = _mapper.Map<IEnumerable<OrderResource>>(pagedResult);

            return Ok(result);
        }
    }
}
