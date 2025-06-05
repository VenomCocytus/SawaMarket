using ProductService.Domain.Models;

namespace ProductService.Contract.Persistence;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product?> GetByNameAsync(string name);
    Task<IReadOnlyList<Product>> GetByCategoryAsync(string categoryId);
}