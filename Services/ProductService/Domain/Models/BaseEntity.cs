using Lombok.NET;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductService.Domain.Models;

[NoArgsConstructor]
[AllArgsConstructor]
public abstract partial class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    
    //TODO: Add a logic to automatically set CreatedAt and UpdatedAt timestamps
    //TODO: Add a logic to automatically set CreatedBy and UpdatedBy user IDs
}