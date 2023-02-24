using Application.UnitTests.Mocks.Services;
using Kolmeo.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.UnitTests
{
    /// <summary>    
    /// Tests setup for ProductController
    /// </summary>
    public class ProductsControllerTests
    {
        private readonly Mock<ILogger<ProductsController>> _logger;
        /// <summary>
        /// ProductsControllerTests Controller
        /// </summary>
        public ProductsControllerTests()
        {
            _logger = new Mock<ILogger<ProductsController>>();
        }

        [Fact(DisplayName = "ProductsFound")]
        public async Task ProductController_ProductsFound()
        {
            //Arrange
            var mockProducts = DataSetupHelper.GetMockProducts();
            var mockProductsService = new MockProductsService().MockGetAllProducts(mockProducts, true);
            var controller = new ProductsController(_logger.Object, mockProductsService.Object);

            //Act
            var result = await controller.GetAsync(string.Empty);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            mockProductsService.VerifyGetAllProducts(Times.Once());
        }
        [Fact(DisplayName = "ProductsFoundWithName")]
        public async Task ProductController_ProductsFoundWithName()
        {
            //Arrange
            var mockProducts = DataSetupHelper.GetMockProducts();
            var mockProductsService = new MockProductsService().MockGetAllProducts(mockProducts, true);
            var controller = new ProductsController(_logger.Object, mockProductsService.Object);

            //Act
            var result = await controller.GetAsync("Apple");

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            mockProductsService.VerifyGetAllProducts(Times.Once());
        }

        [Fact(DisplayName = "AddNewProduct")]
        public async Task ProductController_AddNewProduct()
        {
            //Arrange
            var mockProduct = DataSetupHelper.GetMockProduct();
            var mockProductsService = new MockProductsService().MockAddNewProduct(mockProduct, true);
            var controller = new ProductsController(_logger.Object, mockProductsService.Object);

            //Act
            var result = await controller.PostAsync(mockProduct);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, ((ObjectResult)result).StatusCode);
            Assert.Equal(mockProduct, ((ObjectResult)result).Value);
        }
        [Fact(DisplayName = "Products Not Found")]
        public async Task ProductController_ProductsNotFound()
        {
            //Arrange
            var mockProducts = DataSetupHelper.GetMockProducts();
            var mockProductsService = new MockProductsService().MockGetAllProducts(mockProducts, false);
            var controller = new ProductsController(_logger.Object, mockProductsService.Object);

            //Act
            var result = await controller.GetAsync(string.Empty);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
            mockProductsService.VerifyGetAllProducts(Times.Once());
        }
        [Fact(DisplayName = "AddNewProduct Failure")]
        public async Task ProductController_AddNewProductFailure()
        {
            //Arrange
            var mockProduct = DataSetupHelper.GetMockProduct();
            var mockProductsService = new MockProductsService().MockAddNewProduct(mockProduct, false);
            var controller = new ProductsController(_logger.Object, mockProductsService.Object);

            //Act
            var result = await controller.PostAsync(mockProduct);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
