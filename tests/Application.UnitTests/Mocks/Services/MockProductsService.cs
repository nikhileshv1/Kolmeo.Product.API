using Kolmeo.Application.Interfaces;
using Kolmeo.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Mocks.Services
{
    /// <summary>
    /// Setup for MockProductsService
    /// </summary>
    internal class MockProductsService : Mock<IProductsService>
    {
        internal MockProductsService MockGetAllProducts(Products products, bool productsFound)
        {
            Setup(x => x.GetAllProductsAsync(It.IsAny<string>()))
                .ReturnsAsync(productsFound ? products : null);
            return this;
        }
        internal MockProductsService VerifyGetAllProducts(Times times)
        {
            Verify(x => x.GetAllProductsAsync(It.IsAny<string>()), times);

            return this;
        }
        internal MockProductsService MockAddNewProduct(Product product, bool success)
        {
            Setup(x => x.AddNewProductAsync(product))
                .ReturnsAsync(success);
            return this;
        }
    }
}
