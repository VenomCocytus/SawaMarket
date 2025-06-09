using MediatR;
using ProductService.Application.Features.Products.Commands;
using ProductService.Application.Mappers;
using ProductService.Common.Helper;
using ProductService.Contract.Persistence;
using ProductService.Domain.Models;

namespace ProductService.Application.Handlers;

public class UpdateProductHandler(IProductRepository productRepository) 
    : IRequestHandler<UpdateProductCommand, GenericResponse<bool>>
{
    public Task<GenericResponse<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productToUpdate = ProductMapper.Mapper.Map<Product>(request);
        if (productToUpdate is null)
            throw new ApplicationException("Product.NotFound");
        productRepository.UpdateAsync(request.Id, productToUpdate);

        return Task.FromResult(GenericResponse<bool>
            .Success("Product.Updated.Successfully"));
    }
}