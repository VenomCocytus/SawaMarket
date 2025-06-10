using MediatR;
using ProductService.Application.Features.Products.Queries.Requests;
using ProductService.Application.Mappers;
using ProductService.Contract.Common;
using ProductService.Contract.DTOs;
using ProductService.Contract.Persistence;

namespace ProductService.Application.Features.Products.Queries.Handlers;

public class GetPaginatedProductsHandler(IProductRepository productRepository) 
    : IRequestHandler<GetPaginatedProductsQuery, GenericResponse<PagedResponse<ProductResponse>>>
{
    public async Task<GenericResponse<PagedResponse<ProductResponse>>> Handle(GetPaginatedProductsQuery request, CancellationToken cancellationToken)
    {
        var productList = await productRepository
            .GetPagedProductsAsync(request.ProductPagedRequest);
        
        return GenericResponse<PagedResponse<ProductResponse>>.Success(
            ProductMapper.Mapper.Map<PagedResponse<ProductResponse>>(productList), 
            "Products.Retrieved.Successfully.");
    }
}