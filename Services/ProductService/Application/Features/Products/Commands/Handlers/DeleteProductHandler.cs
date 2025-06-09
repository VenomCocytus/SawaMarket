using MediatR;
using ProductService.Application.Features.Products.Commands.Requests;
using ProductService.Common.Responses;
using ProductService.Contract.Persistence;

namespace ProductService.Application.Features.Products.Commands.Handlers;

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