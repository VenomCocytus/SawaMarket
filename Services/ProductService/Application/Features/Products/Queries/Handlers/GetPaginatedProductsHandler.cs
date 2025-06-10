using MediatR;
using ProductService.Application.Features.Products.Queries.Requests;
using ProductService.Contract.Common;
using ProductService.Contract.DTOs;
using ProductService.Contract.Persistence;

namespace ProductService.Application.Features.Products.Queries.Handlers;

public class GetPaginatedProductsHandler(IProductRepository productRepository) 
    : IRequestHandler<GetPaginatedProductsQuery, GenericResponse<PagedResponse<ProductResponse>>>
{
    public Task<GenericResponse<PagedResponse<ProductResponse>>> Handle(GetPaginatedProductsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("This handler is not implemented yet. Please implement the logic to handle paginated product retrieval.");
    }
}