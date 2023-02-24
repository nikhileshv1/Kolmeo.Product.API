using Kolmeo.Application.Interfaces;
using Kolmeo.Domain.DbEntities;
using Moq;

namespace Application.UnitTests.Mocks.Repositories
{
    /// <summary>
    /// Setup for MockProductRepository
    /// </summary>
    internal class MockProductRepository: Mock<IProductRepository>
    {
        internal MockProductRepository MockGetAllProducts(List<Product> products, bool productsFound)
        {
            Setup(x => x.GetAllProductsAsync(It.IsAny<string>()))
                .ReturnsAsync(productsFound ? products : null);
            return this;
        }
        internal MockProductRepository MockGetProductById(Product product, bool productFound)
        {
            Setup(x => x.GetProductByIdAsync(Guid.Empty))
                .ReturnsAsync(productFound ? product : null);
            return this;
        }
        internal MockProductRepository MockAddNewProduct(bool isSuccess)
        {
            Setup(x => x.AddNewProductAsync(It.IsAny<Product>()))
                .ReturnsAsync(isSuccess);
            return this;
        }
        internal MockProductRepository MockUpdateProduct(bool isSuccess)
        {
            Setup(x => x.UpdateProductAsync(It.IsAny<Guid>(), It.IsAny<Product>()))
                .ReturnsAsync(isSuccess);
            return this;
        }
        internal MockProductRepository MockDeleteProduct(bool isSuccess)
        {
            Setup(x => x.DeleteProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(isSuccess);
            return this;
        }
    }
}
