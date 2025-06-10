namespace ProductService.Contract.Common;

public record PagedResponse<T>(
    int PageNumber,
    int PageSize,
    long TotalCount,
    int TotalPages,
    IReadOnlyList<T> Items
) where T : class
{
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}