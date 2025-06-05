using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ProductService.Infrastructure.Context;

public class ProductDbContext
{
    private readonly IMongoDatabase _productDatabase;
    
    public ProductDbContext(IOptions<ProductDbSettings> settingsOptions)
    {
        var client = new MongoClient(settingsOptions.Value.ConnectionString);
        _productDatabase = client.GetDatabase(settingsOptions.Value.DatabaseName);
    }
    
    public IMongoCollection<T> GetCollection<T>(string name) 
        => _productDatabase.GetCollection<T>(name);
    
    //TODO: Save the creation date of a base entity
    //TODO: Save the update date of a base entity
}