using MediatR;
using ProductService.Application.Features.Products.Commands.Requests;
using ProductService.Application.Mappers;
using ProductService.Contract.Common;
using ProductService.Contract.DTOs;
using ProductService.Contract.Persistence;
using ProductService.Domain.Models;

namespace ProductService.Application.Features.Products.Commands.Handlers;

public class CreateProductHandler(IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand, GenericResponse<ProductResponse>>
{
    public async Task<GenericResponse<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = ProductMapper.Mapper.Map<Product>(request);
        if (productEntity is null)
            throw new ApplicationException("Product.Mapping.Failed");
        var createdProduct = await productRepository.AddAsync(productEntity);
        var productResponse = ProductMapper.Mapper.Map<ProductResponse>(createdProduct);
        
        return GenericResponse<ProductResponse>
            .Success(productResponse, "Product.Created.Successfully");
    }
}