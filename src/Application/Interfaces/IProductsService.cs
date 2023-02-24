using Kolmeo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolmeo.Application.Interfaces
{
    /// <summary>
    /// Interface for IProductsService
    /// </summary>
    public interface IProductsService
    {
        Task<Products?> GetAllProductsAsync(string name);
        Task<Product?> GetProductByIdAsync(Guid Id);
        Task<bool> AddNewProductAsync(Product product);
        Task<bool> UpdateProductAsync(Guid Id, Product updatedProduct);
        Task<bool> DeleteProductAsync(Guid Id);
    }
}
