namespace ProductService.Contract.DTOs;

public record GetProductDto (
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    string CategoryId,
    string ThumbnailUrl,
    string ProductUrl,
    string IsAvailable,
    DateTime CreatedAt
    ){}