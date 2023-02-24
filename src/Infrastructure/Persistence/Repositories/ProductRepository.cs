using Kolmeo.Application.Interfaces;
using Kolmeo.Domain.DbEntities;
using Kolmeo.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kolmeo.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsDbContext _productDbContext;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ProductsDbContext productDbContext, ILogger<ProductRepository> logger)
        {
            _productDbContext = productDbContext ?? throw new ArgumentNullException(nameof(productDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// Adds New Product 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> AddNewProductAsync(Product product)
        {
            try
            {
                var productExists = await _productDbContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
                if (productExists != null)
                {
                    return false;
                }

                _productDbContext.Products.Add(product);
                _productDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error at :" + nameof(AddNewProductAsync) + ex.Message, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Deletes a Product
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProductAsync(Guid Id)
        {
            try
            {
                var productToDelete = await _productDbContext.Products.FirstOrDefaultAsync(product => product.Id == Id);
                if (productToDelete == null)
                {
                    return false;
                }

                _productDbContext.Products.RemoveRange(productToDelete);
                _productDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {                
                _logger.LogError("Error at :" + nameof(DeleteProductAsync) + ex.Message, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Gets All available Products
        /// </summary>
        /// <returns></returns>
        public async Task<List<Product>> GetAllProductsAsync(string name)
        {
            try
            {
                return string.IsNullOrWhiteSpace(name) ? 
                    await _productDbContext.Products.ToListAsync() : 
                    await _productDbContext.Products.Where(product => product.Name.Contains(name)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error at :" + nameof(GetAllProductsAsync) + ex.Message, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Gets All available Products By ProductId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Product?> GetProductByIdAsync(Guid Id)
        {
            try
            {                
                return await _productDbContext.Products.FirstOrDefaultAsync(product => product.Id == Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error at :" + nameof(GetProductByIdAsync) + ex.Message, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Updates a Product 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updatedproduct"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProductAsync(Guid Id, Product updatedproduct)
        {
            try
            {
                var productToUpdate = await _productDbContext.Products
                    .Where(uniqueId => uniqueId.Id == Id)
                    .FirstOrDefaultAsync();

                if(productToUpdate == null) 
                { 
                    return false; 
                }

                productToUpdate.Name = updatedproduct.Name;
                productToUpdate.Description = updatedproduct.Description;
                productToUpdate.Price = updatedproduct.Price;

                _productDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error at :" + nameof(UpdateProductAsync) + ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}
