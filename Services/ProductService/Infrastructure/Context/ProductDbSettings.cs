namespace ProductService.Infrastructure.Context;

public class ProductDbSettings(string connectionString, string databaseName)
{
    public string ConnectionString { get; init; } = connectionString;
    public string DatabaseName { get; init; } = databaseName;
}