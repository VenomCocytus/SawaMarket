using MediatR;
using ProductService.Common.Helper;
using ProductService.Contract.DTOs;

namespace ProductService.Application.Features.Products.Commands;

public record CreateProductCommand(CreateProductDto CreateProductDto) : 
    IRequest<GenericResponse<ProductDto>>
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, GenericResponse<ProductDto>>
    {
        private readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GenericResponse<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.CreateProductAsync(request.CreateProductDto);
        }
    }
}