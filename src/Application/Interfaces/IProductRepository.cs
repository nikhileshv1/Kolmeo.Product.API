using Kolmeo.Domain.DbEntities;

namespace Kolmeo.Application.Interfaces
{
    /// <summary>
    /// Interface for IProductRepository
    /// </summary>
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync(string name);
        Task<Product> GetProductByIdAsync(Guid Id);
        Task<bool> AddNewProductAsync(Product product);
        Task<bool> UpdateProductAsync(Guid Id, Product updatedproduct);
        Task<bool> DeleteProductAsync(Guid Id);
    }
}