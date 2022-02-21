using JET.Application.Interfaces;
using JET.Domain.Entities.Tables;
using JET.Infra.Data.Context;
using JET.Routes;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult> Create([FromBody] ProductCreateOrUpdate body)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _produtoService.Create(body);

                return Created(ApiRoutes.Product.Create, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Problem(detail: ex.StackTrace, title: ex.Message);
            }
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Success when searching for a product</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpGet]
        [Route(ApiRoutes.Product.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = _produtoService.GetById(id);

                return Ok(new { items = result });
            }
            catch (ArgumentException ex)
            {
                return Problem(detail: ex.StackTrace, title: ex.Message);
            }
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Success in searching all products</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpGet]
        [Route(ApiRoutes.Product.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = _produtoService.GetAll();

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
        /// <response code="200">Success when searching for a product</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpPatch]
        [Route(ApiRoutes.Product.Patch)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<Products> body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var productsDB = _produtoService.GetById(id);

                if (productsDB == null)
                    return NotFound("No record found for updated");

                body.ApplyTo(productsDB, ModelState);

                var isValid = TryValidateModel(productsDB);

                var result = await _produtoService.Patch(body);

                return Ok("Resource updated successfully");
            }
            catch (ArgumentException ex)
            {
                return Problem(detail: ex.StackTrace, title: ex.Message);
            }
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Success when searching for a product</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpDelete]
        [Route(ApiRoutes.Product.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var productsDB = _produtoService.GetById(id);

                if (productsDB == null)
                    return NotFound("No record found for delete");

                var result = await _produtoService.Delete(productsDB);

                return Ok("Resource deleted successfully");
            }
            catch (ArgumentException ex)
            {
                return Problem(detail: ex.StackTrace, title: ex.Message);
            }
        }


    }
}
