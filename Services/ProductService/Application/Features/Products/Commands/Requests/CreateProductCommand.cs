using System.ComponentModel.DataAnnotations;
using MediatR;
using ProductService.Contract.Common;
using ProductService.Contract.DTOs;

namespace ProductService.Application.Features.Products.Commands.Requests;

public record CreateProductCommand(
    [Required]
    [StringLength(100, MinimumLength = 3)]
    string Name,

    [StringLength(500)]
    string Description,

    [Required]
    [Range(0.01, 100000)]
    decimal Price,

    [Required]
    [Range(0, int.MaxValue)]
    int StockQuantity,

    [Required]
    [StringLength(50)]
    string CategoryId,

    [Url]
    string ThumbnailUrl,

    [Url]
    string ProductUrl) : IRequest<GenericResponse<ProductResponse>>
{}