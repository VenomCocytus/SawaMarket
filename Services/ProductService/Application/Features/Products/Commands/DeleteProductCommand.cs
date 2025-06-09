using MediatR;
using ProductService.Common.Helper;

namespace ProductService.Application.Features.Products.Commands;

public record  DeleteProductCommand (string Id) : IRequest<GenericResponse<bool>>
{ }