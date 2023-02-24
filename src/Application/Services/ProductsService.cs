using AutoMapper;
using Kolmeo.Application.Interfaces;
using Kolmeo.Domain;
using DbEntities = Kolmeo.Domain.DbEntities;
using Microsoft.Extensions.Logging;

namespace Kolmeo.Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ILogger<ProductsService> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsService(ILogger<ProductsService> logger,IProductRepository productRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Adds New products
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> AddNewProductAsync(Product product)
        {
            try
            {
                var newProduct = _mapper.Map<Product, DbEntities.Product>(product);
                return await _productRepository.AddNewProductAsync(newProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                throw;
            }
        }

        /// <summary>
        /// Deletes a product 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProductAsync(Guid Id)
        {
            try
            {
                return await _productRepository.DeleteProductAsync(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }
        /// <summary>
        /// Gets All available Products 
        /// </summary>
        public async Task<Products?> GetAllProductsAsync(string name)
        {
            var listOfProducts = await _productRepository.GetAllProductsAsync(name);
            if (listOfProducts != null && listOfProducts.Any())
            {
                var result = _mapper.Map<IEnumerable<DbEntities.Product>, IEnumerable<Product>>(listOfProducts);
                var products = new Products()
                {
                    Items = result.ToList()
                };
                FilterProductsByName(products, name);
                return products;
            }
            return null;
        }

        /// <summary>
        /// Gets all available products for a given Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Product?> GetProductByIdAsync(Guid Id)
        {
            var product = await _productRepository.GetProductByIdAsync(Id);
            if (product != null)
            {
                var result = _mapper.Map<DbEntities.Product, Product>(product);
                return result;
            }
            return null;
        }

        /// <summary>
        /// Updates details for a Product
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updatedProduct"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProductAsync(Guid Id, Product updatedProduct)
        {
            try
            {
                var product = _mapper.Map<Product, DbEntities.Product>(updatedProduct);
                return await _productRepository.UpdateProductAsync(Id, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }
        /// <summary>
        /// Filters the list of products based on the search Key 'Name'
        /// </summary>
        /// <param name="products"></param>
        /// <param name="name"></param>
        private void FilterProductsByName(Products products, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var filteredProducts = products?.Items?.FindAll(searchKey => searchKey.Name.ToLower().Contains(name.ToLower()));
                if (products != null && filteredProducts != null)
                {
                    products.Items = filteredProducts;
                }
            }
        }
    }
}
