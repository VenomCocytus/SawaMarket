using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using ProductService.Common.Application.Interfaces;
using ProductService.Contract.Persistence;
using ProductService.Infrastructure.Context;
using ProductService.Infrastructure.Repositories;

namespace ProductService.Common.Extensions;

public static class CommonExtensions
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, IDateTimeProvider>();
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
        
        return services;
    }
    
    /// <summary>
    /// Registers all services related to Product feature including repositories and business services.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="configuration">Application configuration for options binding.</param>
    /// <returns>The IServiceCollection for chaining.</returns>
    public static IServiceCollection AddProductSpecificServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure MongoDB settings from configuration
        services.Configure<ProductDbSettings>(configuration.GetSection("MongoDBSettings"));

        // Register MongoDB context as singleton
        services.AddSingleton<ProductDbContext>();

        // Register repositories
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}