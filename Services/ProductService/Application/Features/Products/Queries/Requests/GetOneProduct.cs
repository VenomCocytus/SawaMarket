using MediatR;
using ProductService.Contract.Common;
using ProductService.Contract.DTOs;

namespace ProductService.Application.Features.Products.Queries.Requests;

public record GetOneProduct(
    string? Id, 
    string? Name) : IRequest<GenericResponse<ProductResponse>>;