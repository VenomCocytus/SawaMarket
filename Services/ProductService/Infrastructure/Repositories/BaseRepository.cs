using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ProductService.Contract.Common;
using ProductService.Contract.Persistence;
using ProductService.Infrastructure.Context;
using BaseEntity = ProductService.Domain.Common.BaseEntity;

namespace ProductService.Infrastructure.Repositories;

public class BaseRepository<T>(ProductDbContext productDbContext, string collectionName) : IBaseRepository<T>
    where T : BaseEntity
{
    protected readonly IMongoCollection<T> ProductDbCollection = productDbContext.GetCollection<T>(collectionName) ?? 
                                                                throw new ArgumentNullException(nameof(productDbContext));
    protected readonly FilterDefinitionBuilder<T> FilterBuilder = Builders<T>.Filter;
    protected readonly SortDefinitionBuilder<T> SortBuilder = Builders<T>.Sort;

    public async Task<IReadOnlyList<T>> GetAllAsync() 
        => await ProductDbCollection.Find(FilterBuilder.Empty).ToListAsync();

    public async Task<PagedResponse<T>> GetPagedAsync(PagedRequest pagedRequest, FilterDefinition<T>? filterBuilder = null, 
        SortDefinition<T>? sortBuilder = null)
    {
        filterBuilder ??= FilterBuilder.Empty;
        sortBuilder ??= SortBuilder.Ascending(x => x.Id);
        
        var totalDocuments = await ProductDbCollection.CountDocumentsAsync(filterBuilder);
        var totalPages = (int) Math.Ceiling(totalDocuments / (double) pagedRequest.PageSize);

        var items = ProductDbCollection
            .Find(filterBuilder)
            .Sort(sortBuilder)
            .Skip(pagedRequest.PageSize * (pagedRequest.PageNumber - 1))
            .Limit(pagedRequest.PageSize)
            .ToListAsync();
        
        return new PagedResponse<T>(
                Items : items.Result,
                TotalCount : totalDocuments,
                PageNumber : pagedRequest.PageNumber,
                PageSize : pagedRequest.PageSize,
                TotalPages : totalPages
            );
    }

    public async Task<IReadOnlyList<T>> GetAsync(FilterDefinition<T>? filterBuilder = null,
        SortDefinition<T>? sortBuilder = null)
    {
        filterBuilder ??= FilterBuilder.Empty;
        sortBuilder ??= SortBuilder.Ascending(x => x.Id);
        
        return await ProductDbCollection.Find(filterBuilder).Sort(sortBuilder).ToListAsync();
    }

    public async Task<bool> ExistsByIdAsync(string id)
    {
        var filter = FilterBuilder.Eq(x => x.Id, id);
        return await ProductDbCollection.Find(filter).AnyAsync();
    }

    public async Task<T?> GetByIdAsync(string? id)
    {
        var filter = FilterBuilder.Eq(x => x.Id, id);
        return await ProductDbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await ProductDbCollection.InsertOneAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(string id, T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var filter = FilterBuilder.Eq(x => x.Id, id);
        await ProductDbCollection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        
        var filter = FilterBuilder.Eq(x => x.Id, id);
        await ProductDbCollection.DeleteOneAsync(filter);
    }
}