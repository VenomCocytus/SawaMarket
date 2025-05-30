using System.ComponentModel.DataAnnotations;
using Lombok.NET;
using ProductService.Common.Models;

namespace ProductService.Domain.Models;

[NoArgsConstructor]
[AllArgsConstructor]
public partial class Product : BaseEntity
{
    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 100 characters.")]
    public string Name { get; init; }

    [StringLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
    public string Description { get; init; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, 100000, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; init; }

    [Required(ErrorMessage = "Stock quantity is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
    public int StockQuantity { get; init; }

    [Required(ErrorMessage = "Category is required.")]
    [StringLength(50)]
    public string Category { get; init; }

    [Url(ErrorMessage = "Thumbnail URL must be a valid URL.")]
    public string ThumbnailUrl { get; init; }

    [Url(ErrorMessage = "Product URL must be a valid URL.")]
    public string ProductUrl { get; init; }
}