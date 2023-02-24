using Application.UnitTests;
using Application.UnitTests.Mocks.Repositories;
using AutoMapper;
using Kolmeo.Application.MapProfiles;
using Kolmeo.Application.Services;
using Kolmeo.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using DbEntities = Kolmeo.Domain.DbEntities;

namespace Kolmeo.Application.UnitTests
{
    public class ProductServiceTests
    {
        private readonly Mock<ILogger<ProductsService>> _logger;
        private static IMapper _mapper;
        /// <summary>
        /// Setup needed for ProductServiceTests
        /// </summary>
        public ProductServiceTests()
        {
            _logger = new Mock<ILogger<ProductsService>>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ProductProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact(DisplayName = "ProductsFound")]
        public async Task ProductService_ProductsFound()
        {
            //Arrange
            var mockProducts = DataSetupHelper.GetMockProducts();
            var translatedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<DbEntities.Product>>(mockProducts.Items);
            var mockProductsDbAccess = new MockProductRepository().MockGetAllProducts(translatedProducts.ToList(), true);

            var service = new ProductsService(_logger.Object, mockProductsDbAccess.Object, _mapper);
            //Act
            var result = await service.GetAllProductsAsync(string.Empty);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(translatedProducts.ToList()[0].Id, result.Items?[0].Id);
            Assert.Equal(translatedProducts.ToList()[0].Price, result.Items?[0].Price);
        }
        [Fact(DisplayName = "ProductsFoundWithName")]
        public async Task ProductService_ProductsFoundWithName()
        {
            //Arrange
            var mockProducts = DataSetupHelper.GetMockProducts();
            var translatedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<DbEntities.Product>>(mockProducts.Items);
            var mockProductsDbAccess = new MockProductRepository().MockGetAllProducts(translatedProducts.ToList(), true);

            var service = new ProductsService(_logger.Object, mockProductsDbAccess.Object, _mapper);
            //Act
            var result = await service.GetAllProductsAsync("samsung");

            //Assert
            Assert.NotNull(result);
            Assert.Equal(translatedProducts.ToList()[1].Id, result.Items?[0].Id);
            Assert.Equal(translatedProducts.ToList()[1].Price, result.Items?[0].Price);
        }
        [Fact(DisplayName = "Product Found By Id")]
        public async Task ProductServiceProductsFoundById()
        {
            //Arrange
            var mockProduct = DataSetupHelper.GetMockProduct();
            var translatedProduct = _mapper.Map<Product, DbEntities.Product>(mockProduct);
            var mockProductsDbAccess = new MockProductRepository().MockGetProductById(translatedProduct, true);

            var service = new ProductsService(_logger.Object, mockProductsDbAccess.Object, _mapper);

            //Act
            var result = await service.GetProductByIdAsync(Guid.Empty);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(translatedProduct.Id, result.Id);
            Assert.Equal(translatedProduct.Price, result.Price);
        }
        [Fact(DisplayName = "AddNewProduct")]
        public async Task ProductService_AddNewProduct()
        {
            //Arrange
            var mockProduct = DataSetupHelper.GetMockProduct();
            var mockProductsDbAccess = new MockProductRepository().MockAddNewProduct(true);

            var service = new ProductsService(_logger.Object, mockProductsDbAccess.Object, _mapper);

            //Act
            var result = await service.AddNewProductAsync(mockProduct);

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Products Not Found")]
        public async Task ProductService_ProductsNotFound()
        {
            //Arrange
            var mockProducts = DataSetupHelper.GetMockProducts();
            var translatedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<DbEntities.Product>>(mockProducts.Items);
            var mockProductsDbAccess = new MockProductRepository().MockGetAllProducts(translatedProducts.ToList(), false);

            var service = new ProductsService(_logger.Object, mockProductsDbAccess.Object, _mapper);
            //Act
            var result = await service.GetAllProductsAsync(string.Empty);

            //Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "ProductsNotFoundWithName")]
        public async Task ProductService_ProductsNotFoundWithName()
        {
            //Arrange
            var mockProducts = DataSetupHelper.GetMockProducts();
            var translatedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<DbEntities.Product>>(mockProducts.Items);
            var mockProductsDbAccess = new MockProductRepository().MockGetAllProducts(translatedProducts.ToList(), true);

            var service = new ProductsService(_logger.Object, mockProductsDbAccess.Object, _mapper);
            //Act
            var result = await service.GetAllProductsAsync("sony");

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result.Items);
        }
        [Fact(DisplayName = "Product Not Found By Id")]
        public async Task ProductServiceProductsNotFoundById()
        {
            //Arrange
            var mockProduct = DataSetupHelper.GetMockProduct();
            var translatedProduct = _mapper.Map<Product, DbEntities.Product>(mockProduct);
            var mockProductsDbAccess = new MockProductRepository().MockGetProductById(translatedProduct, false);

            var service = new ProductsService(_logger.Object, mockProductsDbAccess.Object, _mapper);

            //Act
            var result = await service.GetProductByIdAsync(mockProduct.Id);

            //Assert
            Assert.Null(result);
        }
        [Fact(DisplayName = "AddNewProductFailure")]
        public async Task ProductService_AddNewProductFailure()
        {
            //Arrange
            var mockProduct = DataSetupHelper.GetMockProduct();
            var mockProductsDbAccess = new MockProductRepository().MockAddNewProduct(false);

            var service = new ProductsService(_logger.Object, mockProductsDbAccess.Object, _mapper);

            //Act
            var result = await service.AddNewProductAsync(mockProduct);

            //Assert
            Assert.False(result);
        }
    }
}