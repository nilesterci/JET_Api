using JET.Application.Interfaces;
using JET.Domain.Entities.Tables;
using JET.Routes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JET.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _produtoService;

        public ProductsController(IProductsService produtoservice)
        {
            _produtoService = produtoservice;
        }

        [HttpGet]
        [Route("products")]
        public async Task<ActionResult> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _produtoService.Get();

                return Ok(new { items = result, hasNext = true });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Success creating new product</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpPost]
        [Route(ApiRoutes.Product.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] Products body)
        {
            var command = new Products()
            {
                ProductName = body.ProductName,
                Description = body.Description,
                Stock = body.Stock,
                Status = body.Status,
                Price = body.Price
            };

            await _produtoService.Post(command);

            return Created(ApiRoutes.Product.Create, command);
        }

    }
}
