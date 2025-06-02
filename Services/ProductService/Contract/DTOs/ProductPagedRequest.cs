using ProductService.Common.Helper;

namespace ProductService.Contract.DTOs;

public record ProductPagedRequest
(
    int PageNumber = 1,
    int PageSize = 10,
    string? SearchTerm = null,
    string? CategoryId = null
) : PagedRequest(PageNumber, PageSize)
{ }