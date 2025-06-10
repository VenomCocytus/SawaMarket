using MediatR;
using ProductService.Contract.Common;

namespace ProductService.Application.Features.Products.Commands.Requests;

public record  DeleteProductCommand (string Id) : IRequest<GenericResponse<bool>>
{ }