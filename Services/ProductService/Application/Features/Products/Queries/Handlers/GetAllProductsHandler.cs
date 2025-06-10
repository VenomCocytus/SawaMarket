using MediatR;
using ProductService.Application.Features.Products.Queries.Requests;
using ProductService.Application.Mappers;
using ProductService.Contract.Common;
using ProductService.Contract.DTOs;
using ProductService.Contract.Persistence;

namespace ProductService.Application.Features.Products.Queries.Handlers;

public class GetAllProductsHandler(IProductRepository productRepository) 
    : IRequestHandler<GetAllProductQuery, GenericResponse<IReadOnlyList<ProductResponse>>>
{
    public Task<GenericResponse<IReadOnlyList<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var productsToReturn = productRepository.GetAllAsync();
        
        return productsToReturn.ContinueWith(task => 
            GenericResponse<IReadOnlyList<ProductResponse>>.Success(
                task.Result.Select(product => ProductMapper.Mapper.Map<ProductResponse>(product)).ToList(), 
                "Product.List.Found.Successfully"), cancellationToken);
    }
}