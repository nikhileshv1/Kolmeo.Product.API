using Application.UnitTests;
using Kolmeo.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitTests.Mocks
{
    /// <summary>
    /// Setup for MockProductsDbContext
    /// </summary>
    internal class MockProductsDbContext
    {
        #region Products
        internal DbContextOptions<ProductsDbContext> MockGetAllProducts(bool productsFound)
        {
            var databaseOptions = CreateTestDatabase(string.Concat(nameof(MockGetAllProducts), productsFound));
            using (var context = new ProductsDbContext(databaseOptions))
            {
                if (productsFound)
                    context.AddRange(DataSetupHelper.GetProductsForMockSetup());
                context.SaveChanges();
            }
            return databaseOptions;
        }
        internal DbContextOptions<ProductsDbContext> MockGetProductById(bool productsFound)
        {
            var databaseOptions = CreateTestDatabase(string.Concat(nameof(MockGetProductById), productsFound));
            using (var context = new ProductsDbContext(databaseOptions))
            {
                context.AddRange(DataSetupHelper.GetProductForMockSetup());
                context.SaveChanges();
            }
            return databaseOptions;
        }
        internal DbContextOptions<ProductsDbContext> MockAddNewProduct(bool isSuccess)
        {
            var databaseOptions = CreateTestDatabase(string.Concat(nameof(MockAddNewProduct), isSuccess));
            using (var context = new ProductsDbContext(databaseOptions))
            {
                if (!isSuccess)
                    context.AddRange(DataSetupHelper.GetProductForMockSetup());
                context.SaveChanges();
            }
            return databaseOptions;
        }
        internal DbContextOptions<ProductsDbContext> MockUpdateProduct(bool isSuccess)
        {
            var databaseOptions = CreateTestDatabase(string.Concat(nameof(MockUpdateProduct), isSuccess));
            using (var context = new ProductsDbContext(databaseOptions))
            {
                if (isSuccess)
                    context.AddRange(DataSetupHelper.GetProductForMockSetup());
                context.SaveChanges();
            }
            return databaseOptions;
        }
        internal DbContextOptions<ProductsDbContext> MockDeleteProduct(bool isSuccess)
        {
            var databaseOptions = CreateTestDatabase(string.Concat(nameof(MockDeleteProduct), isSuccess));
            using (var context = new ProductsDbContext(databaseOptions))
            {
                if (isSuccess)
                    context.AddRange(DataSetupHelper.GetProductForMockSetup());
                context.SaveChanges();
            }
            return databaseOptions;
        }
        #endregion

        private DbContextOptions<ProductsDbContext> CreateTestDatabase(string testName)
        {
            //Create In Memory Database            
            return new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase(databaseName: testName)
            .Options;
        }
    }
}
