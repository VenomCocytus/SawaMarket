using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ProductService.Contract.Common;
using ProductService.Contract.DTOs;
using ProductService.Contract.Persistence;
using ProductService.Domain.Models;
using ProductService.Infrastructure.Context;

namespace ProductService.Infrastructure.Repositories;

public sealed class ProductRepository(ProductDbContext productDbContext, string collectionName = nameof(Product)) 
    : BaseRepository<Product>(productDbContext, collectionName), IProductRepository
{
    public async Task<Product?> GetByNameAsync(string name)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Name, name);
        return await ProductDbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<Product>> GetByCategoryIdAsync(string categoryId)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
        return await ProductDbCollection.Find(filter).ToListAsync();
    }

    public async Task<bool> IsNameUniqueAsync(string? id, string name)
    {
        // Check if a product with the given name already exists excluding the current product
        var filter = Builders<Product>.Filter.Eq(p => p.Name, name);
        if (!string.IsNullOrEmpty(id)) filter = Builders<Product>.Filter
            .And(filter, Builders<Product>.Filter.Ne(p => p.Id, id));
        return await ProductDbCollection.Find(filter).CountDocumentsAsync() == 0;
    }

    public async Task<bool> ExistsByCategoryId(string categoryId)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
        return await ProductDbCollection.Find(filter).AnyAsync();
    }

    public async Task<bool> ExistsByName(string name)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Name, name);
        return await ProductDbCollection.Find(filter).AnyAsync();
    }

    public async Task<PagedResponse<Product>> GetPagedProductsAsync(ProductPagedRequest pagedRequest)
    {
        var filterBuilder = FilterBuilder.Empty;

        if (!string.IsNullOrWhiteSpace(pagedRequest.CategoryId))
            filterBuilder &= FilterBuilder.Eq(p => p.CategoryId, pagedRequest.CategoryId);

        if (!string.IsNullOrWhiteSpace(pagedRequest.SearchTerm))
            filterBuilder &= FilterBuilder.Or(
                FilterBuilder.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(pagedRequest.SearchTerm, "i")),
                FilterBuilder.Regex(p => p.Description, new MongoDB.Bson.BsonRegularExpression(pagedRequest.SearchTerm, "i"))
            );
        
        if(!string.IsNullOrWhiteSpace(pagedRequest.MinPrice.ToString()))
            filterBuilder &= FilterBuilder.Gte(p => p.Price, pagedRequest.MinPrice);
        if(!string.IsNullOrWhiteSpace(pagedRequest.MaxPrice.ToString()))
            filterBuilder &= FilterBuilder.Lte(p => p.Price, pagedRequest.MaxPrice);
        
        var sortBuilder = pagedRequest.SortBy switch
        {
            "price" => SortBuilder.Ascending(p => p.Price),
            "createdAt" => SortBuilder.Ascending(p => p.CreatedAt),
            _ => SortBuilder.Ascending(p => p.Name)
        };

        return await GetPagedAsync(pagedRequest, filterBuilder, sortBuilder);
    }
}