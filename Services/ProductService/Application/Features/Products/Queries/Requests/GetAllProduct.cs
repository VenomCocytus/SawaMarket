using MediatR;
using ProductService.Common.Responses;
using ProductService.Contract.DTOs;

namespace ProductService.Application.Features.Products.Queries.Requests;

public record GetAllProduct() : IRequest<GenericResponse<IReadOnlyList<ProductResponse>>>;