using Lombok.NET;

namespace ProductService.Domain.Models;

[NoArgsConstructor]
[AllArgsConstructor]
public partial class Product : BaseEntity
{
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public string CategoryId { get; init; }
    public string ThumbnailUrl { get; init; }
    public string ProductUrl { get; init; }
}