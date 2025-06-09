using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ProductService.API.Middleware;

namespace ProductService.Common.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        
        //TODO: Register FluentValidation validators from all assemblies
        services.AddValidatorsFromAssemblyContaining<Program>();
        services.AddControllers(option => option.Filters.Add<ValidationMiddleware>());
        services.Configure<ApiBehaviorOptions>(options =>
        {
            //TODO: Remove this when all controllers are updated to use FluentValidation
            //TODO: Check the pertinence of this setting
            options.SuppressModelStateInvalidFilter = true; // Disable default model state validation
        });

        return services;
    }
}