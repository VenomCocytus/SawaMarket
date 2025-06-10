using MediatR;
using ProductService.Application.Features.Products.Queries.Requests;
using ProductService.Application.Mappers;
using ProductService.Contract.Common;
using ProductService.Contract.DTOs;
using ProductService.Contract.Persistence;

namespace ProductService.Application.Features.Products.Queries.Handlers;

public class GetOneProductHandler(IProductRepository productRepository) 
    : IRequestHandler<GetOneProduct, GenericResponse<ProductResponse>>
{
    public async Task<GenericResponse<ProductResponse>> Handle(GetOneProduct request, CancellationToken cancellationToken)
    {
        var productToReturn = !string.IsNullOrEmpty(request.Name) 
            ? await productRepository.GetByNameAsync(request.Name)
            : await productRepository.GetByIdAsync(request.Id);

        return GenericResponse<ProductResponse>.Success(
            ProductMapper.Mapper.Map<ProductResponse>(
                productToReturn), "Product.Found.Successfully");
    }
}