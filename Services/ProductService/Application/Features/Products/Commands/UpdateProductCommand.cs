using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProductService.Common.Helper;

namespace ProductService.Application.Features.Products.Commands;

public record UpdateProductCommand(
    [property:BsonId]
    [property:BsonRepresentation(BsonType.ObjectId)]
    string Id,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    string CategoryId
    ) : IRequest<GenericResponse<bool>>
{ }