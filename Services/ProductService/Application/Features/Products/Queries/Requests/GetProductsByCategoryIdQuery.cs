using MediatR;
using ProductService.Contract.Common;
using ProductService.Contract.DTOs;

namespace ProductService.Application.Features.Products.Queries.Requests;

public record GetProductsByCategoryIdQuery(string? CategoryId) : IRequest<GenericResponse<IReadOnlyList<ProductResponse>>>;