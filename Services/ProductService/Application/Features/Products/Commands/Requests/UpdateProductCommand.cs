using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProductService.Contract.Common;

namespace ProductService.Application.Features.Products.Commands.Requests;

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