using MongoDB.Driver;
using ProductService.Contract.Persistence;
using ProductService.Domain.Models;
using ProductService.Infrastructure.Context;

namespace ProductService.Infrastructure.Repositories;

public class ProductRepository(ProductDbContext productDbContext, string collectionName = nameof(Product)) 
    : BaseRepository<Product>(productDbContext, collectionName), IProductRepository
{
    public async Task<Product?> GetByNameAsync(string name)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Name, name);
        return await ProductDbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<Product>> GetByCategoryAsync(string categoryId)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
        return await ProductDbCollection.Find(filter).ToListAsync();
    }
}