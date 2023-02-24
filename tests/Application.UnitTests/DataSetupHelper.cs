using Kolmeo.Domain;
using Kolmeo.Domain.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbProduct = Kolmeo.Domain.DbEntities.Product;
using Product = Kolmeo.Domain.Product;

namespace Application.UnitTests
{
    /// <summary>
    /// DataSetupHelper for the Unit Tests
    /// </summary>
    internal static class DataSetupHelper
    {
        internal static Products GetMockProducts()
        {
            var mockProductApple = new Product
            {
                Id = new Guid("b16e0ce2-8f28-4499-8837-ca020db0b288"),
                Name = "Apple iphone X",
                Description = "Apple iphone X Series phone",
                Price = (decimal)550.00
            };

            var mockProductSamsung = new Product
            {
                Id = new Guid("a68eda7e-1690-42ba-b0dd-9e27c3122528"),
                Name = "Samsung Phone S5",
                Description = "Samsung Phone",
                Price = (decimal)450.00
            };

            var listOfProducts = new List<Product>
            {
                mockProductApple,
                mockProductSamsung
            };

            var mockProducts = new Products()
            {
                Items = listOfProducts
            };

            return mockProducts;
        }

        internal static Product GetMockProduct()
        {
            return new Product
            {
                Id = new Guid("b16e0ce2-8f28-4499-8837-ca020db0b288"),
                Name = "Apple iphone X",
                Description = "Apple iphone X Series phone",
                Price = (decimal)550.00
            };
        }

        //InMemoryDatabase
        internal static List<DbProduct> GetProductsForMockSetup()
        {
            var mockProductApple = new DbProduct
            {
                Id = new Guid("b16e0ce2-8f28-4499-8837-ca020db0b288"),
                Name = "Apple iphone X",
                Description = "Apple iphone X Series phone",
                Price = (decimal)550.00
            };

            var mockProductSamsung = new DbProduct
            {
                Id = new Guid("a68eda7e-1690-42ba-b0dd-9e27c3122528"),
                Name = "Samsung Phone S5",
                Description = "Samsung Phone",
                Price = (decimal)450.00
            };

            var listOfProducts = new List<DbProduct>
            {
                mockProductApple,
                mockProductSamsung
            };

            return listOfProducts;
        }
        internal static DbProduct GetProductForMockSetup()
        {
            return new DbProduct
            {
                Id = new Guid("b16e0ce2-8f28-4499-8837-ca020db0b288"),
                Name = "Apple iphone X",
                Description = "Apple iphone X Series phone",
                Price = (decimal)550.00
            };
        }
    }
}
