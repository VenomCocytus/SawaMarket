using MediatR;
using ProductService.Application.Mappers;
using ProductService.Common.Helper;
using ProductService.Contract.DTOs;
using ProductService.Contract.Persistence;
using ProductService.Domain.Models;

namespace ProductService.Application.Features.Products.Commands;

public class CreateProductHandler(IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand, GenericResponse<ProductResponse>>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<GenericResponse<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = ProductMapper.Mapper.Map<Product>(request);
        if (productEntity is null)
            throw new ApplicationException("There is an issue with mapping while creating new product");
        var createdProduct = await _productRepository.AddAsync(productEntity);
        var productResponse = ProductMapper.Mapper.Map<ProductResponse>(createdProduct);
        
        return GenericResponse<ProductResponse>.Success(productResponse);
    }
}