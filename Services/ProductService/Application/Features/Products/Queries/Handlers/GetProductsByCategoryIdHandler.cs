using MediatR;
using ProductService.Application.Features.Products.Queries.Requests;
using ProductService.Application.Mappers;
using ProductService.Contract.Common;
using ProductService.Contract.DTOs;
using ProductService.Contract.Persistence;

namespace ProductService.Application.Features.Products.Queries.Handlers;

public class GetProductsByCategoryIdHandler(IProductRepository productRepository) 
    : IRequestHandler<GetProductsByCategoryIdQuery, GenericResponse<IReadOnlyList<ProductResponse>>>
{
    public async Task<GenericResponse<IReadOnlyList<ProductResponse>>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var productList = await productRepository.GetByCategoryIdAsync(request.CategoryId);
        
        return GenericResponse<IReadOnlyList<ProductResponse>>.Success(
            productList.Select(ProductMapper.Mapper.Map<ProductResponse>).ToList(),
            "Product.Found.Successfully");
    }
}