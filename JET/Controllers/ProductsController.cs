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
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductsService produtoservice, ILogger<ProductsController> logger)
        {
            _produtoService = produtoservice;
            _logger = logger;
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
                var result = await _produtoService.Get(null);

                return Ok(new { items = result, hasNext = true });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("products/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _produtoService.Get(id);

                return Ok(new { items = result, hasNext = true });
            }
            catch (ArgumentException ex)
            {
                return Problem(
    detail: ex.StackTrace,
    title: ex.Message);
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
        public async Task<ActionResult> Create([FromBody] Products body)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex.InnerException;
            }
        }
    }
}
