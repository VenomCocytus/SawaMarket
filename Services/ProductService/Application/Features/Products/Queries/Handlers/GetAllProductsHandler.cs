using MediatR;
using ProductService.Application.Features.Products.Queries.Requests;
using ProductService.Application.Mappers;
using ProductService.Contract.Common;
using ProductService.Contract.DTOs;
using ProductService.Contract.Persistence;

namespace ProductService.Application.Features.Products.Queries.Handlers;

public class GetAllProductsHandler(IProductRepository productRepository) 
    : IRequestHandler<GetAllProduct, GenericResponse<IReadOnlyList<ProductResponse>>>
{
    public Task<GenericResponse<IReadOnlyList<ProductResponse>>> Handle(GetAllProduct request, CancellationToken cancellationToken)
    {
        var productList = productRepository.GetAllAsync();
        
        return productList.ContinueWith(task => 
            GenericResponse<IReadOnlyList<ProductResponse>>.Success(
                task.Result.Select(product => ProductMapper.Mapper.Map<ProductResponse>(product)).ToList(), 
                "Product.List.Found.Successfully"), cancellationToken);
    }
}