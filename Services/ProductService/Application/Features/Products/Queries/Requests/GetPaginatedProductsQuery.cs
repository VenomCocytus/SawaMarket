using MediatR;
using ProductService.Contract.Common;
using ProductService.Contract.DTOs;

namespace ProductService.Application.Features.Products.Queries.Requests;

public record GetPaginatedProductsQuery(ProductPagedRequest ProductPagedRequest) 
    : IRequest<GenericResponse<PagedResponse<ProductResponse>>>;