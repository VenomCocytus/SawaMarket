using MediatR;
using ProductService.Contract.Common;
using ProductService.Contract.DTOs;

namespace ProductService.Application.Features.Products.Queries.Requests;

public record GetAllProductQuery() : IRequest<GenericResponse<IReadOnlyList<ProductResponse>>>;