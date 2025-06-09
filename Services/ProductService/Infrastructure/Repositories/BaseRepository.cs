using System.Linq.Expressions;
using ProductService.Common.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ProductService.Contract.Persistence;
using ProductService.Infrastructure.Context;

namespace ProductService.Infrastructure.Repositories;

public class BaseRepository<T>(ProductDbContext productDbContext, string collectionName) : IBaseRepository<T>
    where T : BaseEntity
{
    protected readonly IMongoCollection<T> ProductDbCollection = productDbContext.GetCollection<T>(collectionName) ?? 
                                                                throw new ArgumentNullException(nameof(productDbContext));
    private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

    public async Task<IReadOnlyList<T>> GetAllAsync() 
        => await ProductDbCollection.Find(_filterBuilder.Empty).ToListAsync();

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        var filter = _filterBuilder.Where(predicate);
        return await ProductDbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeString = null,
        bool disableTracking = true)
    {
        IQueryable<T> query = ProductDbCollection.AsQueryable();
        if (disableTracking) query = query.AsNoTracking();
        if(!string.IsNullOrEmpty(includeString)) query = query.Include(includeString);
        if(predicate != null) query = query.Where(predicate);
        if (orderBy != null) return await orderBy(query).ToListAsync();
        
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = ProductDbCollection.AsQueryable();
        if(disableTracking) query = query.AsNoTracking();
        if(includes != null) query = includes.Aggregate(query, (current, include) => 
            current.Include(include));
        if(predicate != null) query = query.Where(predicate);
        if(orderBy != null) return await orderBy(query).ToListAsync();
        
        return await query.ToListAsync();
    }

    public async Task<bool> ExistsByIdAsync(string id)
    {
        var filter = _filterBuilder.Eq(x => x.Id, id);
        return await ProductDbCollection.Find(filter).AnyAsync();
    }

    public async Task<T?> GetByIdAsync(string? id)
    {
        var filter = _filterBuilder.Eq(x => x.Id, id);
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

        var filter = _filterBuilder.Eq(x => x.Id, id);
        await ProductDbCollection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        
        var filter = _filterBuilder.Eq(x => x.Id, id);
        await ProductDbCollection.DeleteOneAsync(filter);
    }
}