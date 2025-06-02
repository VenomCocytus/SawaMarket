using System.ComponentModel.DataAnnotations;

namespace ProductService.Contract.DTOs;

public record CreateProductDto(
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
    string ProductUrl)
{}