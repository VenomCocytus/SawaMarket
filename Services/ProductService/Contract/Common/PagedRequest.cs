namespace ProductService.Contract.Common;

public record PagedRequest(
    int PageNumber = 1,
    int PageSize = 10
)
{
    private const int MaxPageSize = 100; //TODO: Consider making this configurable
    public int PageNumber { get; init; } = PageNumber < 1 ? 1 : PageNumber;
    
    public int PageSize { get; init; } = PageSize switch
    {
        < 1 => 10,
        > MaxPageSize => MaxPageSize, 
        _ => PageSize
    };
}
