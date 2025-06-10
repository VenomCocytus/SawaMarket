using ProductService.Contract.Common;

namespace ProductService.Contract.DTOs;

public record ProductPagedRequest : PagedRequest
{
    public string? CategoryId = null;
    public string? SearchTerm = null;
    public string? SortBy = null;
    public decimal? MinPrice = null;
    public decimal? MaxPrice = null;
}