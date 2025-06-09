using MediatR;
using ProductService.Application.Features.Products.Commands;
using ProductService.Common.Helper;
using ProductService.Contract.Persistence;

namespace ProductService.Application.Features.Products.Handlers;

public class DeleteProductHandler(IProductRepository productRepository) 
    : IRequestHandler<DeleteProductCommand, GenericResponse<bool>>
{
    public Task<GenericResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        productRepository.DeleteAsync(request.Id);
        
        return Task.FromResult(GenericResponse<bool>
            .Success(true, "Product.Deleted.Successfully"));
    }
}