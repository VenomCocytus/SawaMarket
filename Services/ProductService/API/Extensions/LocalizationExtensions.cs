using System.Globalization;
using Microsoft.AspNetCore.Localization;
using ProductService.Application.Interfaces;
using ProductService.Application.Services;

namespace ProductService.API.Extensions;

public static class LocalizationExtensions
{
    public static IServiceCollection AddCustomLocalization(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = configuration.GetSection("SupportedCultures").Get<string[]>() 
                ?? ["en-US", "fr-FR", "es-ES", "de-DE", "zh-CN", "ja-JP"];
            options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
            options.SupportedCultures = supportedCultures.Select(culture => new CultureInfo(culture)).ToList();
            options.SupportedUICultures = options.SupportedCultures;
            
            options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
            {
                var request = context.Request;
                var userCulture = request.Headers.AcceptLanguage
                    .ToString().Split(',').FirstOrDefault()?.Trim();
                
                if (string.IsNullOrEmpty(userCulture) 
                    || options.SupportedCultures.All(c => c.Name != userCulture))
                {
                    userCulture = options.DefaultRequestCulture.Culture.Name;
                }

                return await Task.FromResult(new ProviderCultureResult(userCulture, userCulture));
            }));
        });
        
        services.AddScoped<ILocalizationService, LocalizationService>();
        return services;
    }
    
    
}