using JET.Application.Interfaces;
using JET.Domain.Entities.Tables;
using JET.Infra.Data.Context;
using JET.Routes;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

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
        [HttpPost, DisableRequestSizeLimit]
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

                var entity = new Products()
                {
                    ProductName = body.ProductName,
                    Image = body.Image,
                    Description = body.Description,
                    Stock = body.Stock,
                    Status = body.Status,
                    StatusPromo = body.StatusPromo,
                    Price = body.Price
                };

                var result = await _produtoService.Create(entity);

                return Created(ApiRoutes.Product.Create, new { message = "Produto cadastrado com sucesso", items = result });


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
        public ActionResult GetAll([FromQuery] bool status, [FromQuery] string? search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = _produtoService.GetAll(status, search);

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
                    return Ok(new { message = "Não foi encontrado produto para ser atualizado" });

                body.ApplyTo(productsDB, ModelState);

                var isValid = TryValidateModel(productsDB);

                var result = await _produtoService.Patch(body);

                return Ok(new
                {
                    message = "Produto atualizado com sucesso"
                });
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
                    return NotFound(new { message = "Produto não encontrado para ser deletado" });

                var result = await _produtoService.Delete(productsDB);

                return Ok(new { message = "Produto deletado com sucesso" });
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
        /// <response code="201">Success creating new product</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpPost, DisableRequestSizeLimit]
        [Route(ApiRoutes.Product.SendImage)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ResponseImage PostFile(IFormFile fileImage)
        {
            try
            {
                var fileName = "";

                if (fileImage != null)
                {
                    var file = Request.Form.Files[0];
                    var folderName = Path.Combine("assets", "images");
                    var dirName = "C:\\Users\\Nil\\Desktop\\JET angular\\jet\\src\\";
                    var pathToSave = Path.Combine(dirName, folderName);

                    if (file.Length > 0)
                    {
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }
                return new ResponseImage { NameFile = fileName, Response = "Imagem enviada" };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
