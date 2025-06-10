using ProductService.Contract.Common;
using ProductService.Contract.DTOs;
using ProductService.Domain.Models;

namespace ProductService.Contract.Persistence;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product?> GetByNameAsync(string name);
    Task<IReadOnlyList<Product>> GetByCategoryIdAsync(string? categoryId);
    Task<bool> IsNameUniqueAsync(string? id, string name);
    Task<bool> ExistsByCategoryId(string categoryId);
    Task<bool> ExistsByName(string name);
    Task<PagedResponse<Product>> GetPagedProductsAsync(ProductPagedRequest pagedRequest);
}