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
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;

        public ProductsController(IProductsService productsService, IMapper mapper)
        {
            _productsService = productsService;
            _mapper = mapper;
        }

        //GET
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts([FromQuery] ProductQuery query)
        {
            var pagedResult = await _productsService.GetProductsQuery(query);

            HttpContext.Response.Headers.Add("x-total-count", pagedResult.TotalCount.ToString());

            return Ok(_mapper.Map<IEnumerable<ProductResource>>(pagedResult.Items));
        }
    }
}
