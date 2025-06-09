using MediatR;
using ProductService.Common.Responses;

namespace ProductService.Application.Features.Products.Commands.Requests;

public record  DeleteProductCommand (string Id) : IRequest<GenericResponse<bool>>
{ }