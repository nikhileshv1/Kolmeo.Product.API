using Application.UnitTests;
using Infrastructure.UnitTests.Mocks;
using Kolmeo.Infrastructure.Persistence.Context;
using Kolmeo.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Infrastructure.UnitTests
{
    /// <summary>
    /// Tests for Product Repository
    /// </summary>
    public class ProductRepositoryTests
    {
        private readonly Mock<ILogger<ProductRepository>> _logger;
        public ProductRepositoryTests()
        {
            _logger = new Mock<ILogger<ProductRepository>>();
        }

        [Fact(DisplayName = "ProductsFound")]
        public async Task ProductRepository_ProductsFound()
        {
            //Arrange
            var mockProductRepository = new MockProductsDbContext().MockGetAllProducts(true);
            using var context = new ProductsDbContext(mockProductRepository);
            var productsrepository = new ProductRepository(context, _logger.Object);
            //Act
            var result = await productsrepository.GetAllProductsAsync(It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact(DisplayName = "Product Found By Id")]
        public async Task ProductRepositoryProductsFoundById()
        {
            //Arrange
            var mockProductRepository = new MockProductsDbContext().MockGetProductById(true);
            using var context = new ProductsDbContext(mockProductRepository);
            var ProductRepository = new ProductRepository(context, _logger.Object);
            //Act
            var result = await ProductRepository.GetProductByIdAsync(DataSetupHelper.GetProductForMockSetup().Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(DataSetupHelper.GetProductForMockSetup().Id, result.Id);
            Assert.Equal(DataSetupHelper.GetProductForMockSetup().Name, result.Name);
            Assert.Equal(DataSetupHelper.GetProductForMockSetup().Description, result.Description);
            Assert.Equal(DataSetupHelper.GetProductForMockSetup().Price, result.Price);
        }
        [Fact(DisplayName = "AddNewProduct")]
        public async Task ProductRepository_AddNewProduct()
        {
            //Arrange
            var mockProductRepository = new MockProductsDbContext().MockAddNewProduct(true);
            using var context = new ProductsDbContext(mockProductRepository);
            var ProductRepository = new ProductRepository(context, _logger.Object);
            //Act
            var result = await ProductRepository.AddNewProductAsync(DataSetupHelper.GetProductForMockSetup());

            //Assert
            Assert.True(result);
        }
        [Fact(DisplayName = "Update Product")]
        public async Task ProductRepository_UpdateProduct()
        {
            //Arrange
            var mockProductRepository = new MockProductsDbContext().MockUpdateProduct(true);
            using var context = new ProductsDbContext(mockProductRepository);
            var ProductRepository = new ProductRepository(context, _logger.Object);

            //Act
            var result = await ProductRepository.UpdateProductAsync
                (DataSetupHelper.GetProductForMockSetup().Id, DataSetupHelper.GetProductForMockSetup());

            //Assert
            Assert.True(result);
        }
        [Fact(DisplayName = "Delete Product")]
        public async Task ProductRepository_DeleteProduct()
        {
            //Arrange            
            var mockProductRepository = new MockProductsDbContext().MockDeleteProduct(true);
            using var context = new ProductsDbContext(mockProductRepository);
            var ProductRepository = new ProductRepository(context, _logger.Object);

            //Act
            var result = await ProductRepository.DeleteProductAsync(DataSetupHelper.GetProductForMockSetup().Id);

            //Assert
            Assert.True(result);
        }


        [Fact(DisplayName = "ProductsNotFound")]
        public async Task ProductRepository_ProductsNotFound()
        {
            //Arrange
            var mockProductRepository = new MockProductsDbContext().MockGetAllProducts(false);
            using var context = new ProductsDbContext(mockProductRepository);
            var ProductRepository = new ProductRepository(context, _logger.Object);
            //Act
            var result = await ProductRepository.GetAllProductsAsync(It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
        [Fact(DisplayName = "Product Not Found By Id")]
        public async Task ProductRepositoryProductsNotFoundById()
        {
            //Arrange
            var mockProductRepository = new MockProductsDbContext().MockGetProductById(false);
            using var context = new ProductsDbContext(mockProductRepository);
            var ProductRepository = new ProductRepository(context, _logger.Object);
            //Act            
            var result = await ProductRepository.GetProductByIdAsync(It.IsAny<Guid>());

            //Assert
            Assert.Null(result);
        }
        [Fact(DisplayName = "AddNewProductFailure")]
        public async Task ProductRepository_AddNewProductFailure()
        {
            //Arrange
            var mockProductRepository = new MockProductsDbContext().MockAddNewProduct(false);
            using var context = new ProductsDbContext(mockProductRepository);
            var ProductRepository = new ProductRepository(context, _logger.Object);
            //Act
            var result = await ProductRepository.AddNewProductAsync(DataSetupHelper.GetProductForMockSetup());

            //Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Update Product Failure")]
        public async Task ProductRepository_UpdateProductFailure()
        {
            //Arrange
            var mockProductRepository = new MockProductsDbContext().MockUpdateProduct(false);
            using var context = new ProductsDbContext(mockProductRepository);
            var ProductRepository = new ProductRepository(context, _logger.Object);

            //Act
            var result = await ProductRepository.UpdateProductAsync
                (DataSetupHelper.GetProductForMockSetup().Id, DataSetupHelper.GetProductForMockSetup());

            //Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Delete Product Failure")]
        public async Task ProductRepository_DeleteProductFailure()
        {
            //Arrange            
            var mockProductRepository = new MockProductsDbContext().MockDeleteProduct(false);
            using var context = new ProductsDbContext(mockProductRepository);
            var ProductRepository = new ProductRepository(context, _logger.Object);

            //Act
            var result = await ProductRepository.DeleteProductAsync(DataSetupHelper.GetProductForMockSetup().Id);

            //Assert
            Assert.False(result);
        }
    }
}