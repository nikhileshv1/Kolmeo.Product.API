using Kolmeo.Application.Interfaces;
using Kolmeo.Application.Services;
using Kolmeo.Domain;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kolmeo.WebApi.Controllers
{
    /// <summary>
    /// ProductsController
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsService _productsService;
        /// <summary>
        /// ProductsController
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productsService"></param>
        public ProductsController(ILogger<ProductsController> logger,IProductsService productsService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productsService = productsService ?? throw new ArgumentNullException(nameof(productsService));
        }

        /// <summary>
        /// Gets all products.
        /// Finds all products matching the specified name.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(200, "Success", typeof(Products))]
        [SwaggerResponse(404, "NotFound")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAsync([FromQuery] string? name)
        {
            _logger.LogInformation("Get All Products of name {name}:Request Received", name);

            var products = await _productsService.GetAllProductsAsync(name);
            if (products != null)
            {
                _logger.LogInformation("Get All Products of name {name}:Response Sent",name);
                return Ok(products);
            }
            _logger?.LogInformation("Get All Products of name {name}:No products found", name);
            return NotFound();
        }

        /// <summary>
        /// Gets the project that matches the specified ID - ID is a GUID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Success", typeof(Product))]
        [SwaggerResponse(404, "NotFound")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            _logger?.LogInformation($"Get Product for product Id: {id} :Request Received");

            var product = await _productsService.GetProductByIdAsync(id);
            if (product != null)
            {
                _logger?.LogInformation($"Get Product for product Id {id} :Response Sent");
                return Ok(product);
            }
            _logger?.LogInformation($"Get Product for product Id {id} :No products found");
            return NotFound();
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(201, "Created", typeof(Product))]
        [SwaggerResponse(400, "BadRequest")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> PostAsync(Product product)
        {
            _logger.LogInformation("Add Product Id {product.Id} :Request Received", product.Id);            
            var validData = Extensions.IsModelValid(product);
            if (!validData)
            {
                _logger.LogInformation("Add Product Id {product.Id} : Invalid product data.", product.Id);
                return BadRequest();
            }
            var saveSuccess = await _productsService.AddNewProductAsync(product);
            if (saveSuccess)
            {
                _logger?.LogInformation("Add Product Id {product.Id} :Add Successful", product.Id);
                return new ObjectResult(product) { StatusCode = StatusCodes.Status201Created };
            }
            _logger?.LogInformation("Add Product Id {product.Id} : Invalid product data.", product.Id);
            return BadRequest();
        }

        // PUT api/<ProductController>/5
        
        [HttpPut("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Delete(int id)
        {
        }
    }
}
