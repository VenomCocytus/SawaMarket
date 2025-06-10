using MediatR;
using ProductService.Contract.Common;
using ProductService.Contract.DTOs;

namespace ProductService.Application.Features.Products.Queries.Requests;

public record GetAllProduct() : IRequest<GenericResponse<IReadOnlyList<ProductResponse>>>;