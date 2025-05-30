using System.ComponentModel.DataAnnotations;

namespace ProductService.Application.DTOs;

public record GetProductDto (
    int Id,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    string Category,
    string ThumbnailUrl,
    string ProductUrl
    ){}