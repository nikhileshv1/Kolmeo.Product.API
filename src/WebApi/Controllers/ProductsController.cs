using Kolmeo.Application.Interfaces;
using Kolmeo.Domain;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

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

            var products = await _productsService.GetAllProductsAsync(string.IsNullOrWhiteSpace(name) ? string.Empty : name);
            if (products != null)
            {
                _logger.LogInformation("Get All Products of name {name}:Response Sent",name);
                return Ok(products);
            }
            _logger?.LogInformation("Get All Products of name {name}:No products found", name);
            return NotFound();
        }

        /// <summary>
        /// Gets the product that matches the specified ID - ID is a GUID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Success", typeof(Product))]
        [SwaggerResponse(404, "NotFound")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            _logger?.LogInformation("Get Product for product Id: {id} :Request Received", id    );

            var product = await _productsService.GetProductByIdAsync(id);
            if (product != null)
            {
                _logger?.LogInformation("Get Product for product Id {id} :Response Sent", id);
                return Ok(product);
            }
            _logger?.LogInformation("Get Product for product Id {id} :No products found", id);
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

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerResponse(200, "Success", typeof(Product))]
        [SwaggerResponse(400, "BadRequest")]
        [Produces(MediaTypeNames.Application.Json)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [DevelopmentOnly]
        public async Task<IActionResult> UpdateAsync(Guid id, Product product)
        {
            _logger?.LogInformation("Update Product {product.Id} :Request Received", product.Id);
            var validData = Extensions.IsModelValid(product);
            if (!validData)
            {
                _logger?.LogInformation("Update Product {product.Id} :Invalid product data.", product.Id);
                return BadRequest();
            }
            var updateSuccess = await _productsService.UpdateProductAsync(id, product);
            if (updateSuccess)
            {
                _logger?.LogInformation("Update Product {product.Id} :Product Updated",product.Id);
                return Ok(product);
            }
            _logger?.LogInformation($"Update Product {product.Id} :Invalid product data.");
            return BadRequest();
        }


        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Success", typeof(bool))]
        [SwaggerResponse(404, "NotFound")]
        [Produces(MediaTypeNames.Application.Json)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [DevelopmentOnly]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            _logger?.LogInformation("Delete Product {id} :Request Received", id);
            var deleteStatus = await _productsService.DeleteProductAsync(id);
            if (deleteStatus)
            {
                _logger?.LogInformation("Delete Product {id} :Product Deleted", id);
                return NoContent();
            }
            _logger?.LogInformation("Delete Product {id} : Product Not Found", id);
            return NotFound();
        }
    }
}
