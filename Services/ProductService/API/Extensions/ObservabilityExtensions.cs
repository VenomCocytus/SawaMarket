namespace ProductService.API.Extensions;

public static class ObservabilityExtensions
{
    // public static IServiceCollection AddObservability(this IServiceCollection services, IConfiguration configuration)
    // {
    //     services.AddSerilog((serviceProvider, config) =>
    //     {
    //         config.ReadFrom.Configuration(config)
    //             .Enrich.FromLogContext()
    //             .Enrich.WithCorrelationId()
    //             .Enrich.WithExceptionDetails()
    //             .WriteTo.Console()
    //             .WriteTo.File("logs/sawa-market-log-.txt", rollingInterval: RollingInterval.Day)
    //             .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(config["ElasticConfiguration:Uri"]))
    //             {
    //                 AutoRegisterTemplate = true,
    //                 IndexFormat = "sawa-market-log-{0:yyyy.MM.dd}",
    //                 MinimumLogEventLevel = Serilog.Events.LogEventLevel.Information
    //             });
    //     });
    //
    //     services.AddOpenTelemetry()
    //         .WithLogging()
    //         .WithMetrics(builder => builder
    //             .AddInstrumentation()
    //             .AddHttpClientInstrumentation()
    //             .AddPrometheusExporter())
    //         .WithTracing(builder => builder
    //             .AddAspNetCoreInstrumentation()
    //             .AddHttpClientInstrumentation()
    //             .AddEnityFrameworkCoreInstrumentation()
    //             .AddJaegerExporter());
    //
    //     services.AddHealthChecks()
    //         .AddDbContext<SawaMarketDBContext>(name: "EcommerceDBContext", failureStatus: HealthStatus.Degraded)
    //         .AddRedis(configuration.GetConnectionString("RedisConnection"), name: "Redis",
    //             failureStatus: HealthStatus.Degraded)
    //         .AddRedisCache(configuration["RedisConfiguration:ConnectionString"], name: "RedisCache",
    //             failureStatus: HealthStatus.Degraded)
    //         .AddElasticsearch(new Uri(configuration["ElasticConfiguration:Uri"] ?? string.Empty), name: "Elasticsearch",
    //             failureStatus: HealthStatus.Degraded);
    //     
    //     return services;
    // }
}