using System.Linq.Expressions;
using MongoDB.Driver;
using ProductService.Contract.Common;
using BaseEntity = ProductService.Domain.Common.BaseEntity;

namespace ProductService.Contract.Persistence;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<PagedResponse<T>> GetPagedAsync(PagedRequest pagedRequest,
        FilterDefinition<T>? filterBuilder,
        SortDefinition<T>? sortBuilder);
    Task<IReadOnlyList<T>> GetAsync(FilterDefinition<T>? filterBuilder,
        SortDefinition<T>? sortBuilder);
    Task<bool> ExistsByIdAsync(string id);
    Task<T?> GetByIdAsync(string? id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(string id, T entity);
    Task DeleteAsync(string id);
}