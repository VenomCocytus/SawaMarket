using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using ProductService.Application.Interfaces;

namespace ProductService.Application.Services;

public class LocalizationService(IStringLocalizer localizer) : ILocalizationService
{
    private readonly CultureInfo? _currentCultureInfo;
    private readonly IStringLocalizer _localizer = localizer;
    
    public LocalizationService(IStringLocalizerFactory factory, 
        IHttpContextAccessor httpContextAccessor, IStringLocalizer localizer) : this(localizer)
    {
        _localizer = factory.Create("SharedResources",
            typeof(LocalizationService).Assembly.GetName().Name!);
        _currentCultureInfo = httpContextAccessor.HttpContext?.Features
            .Get<IRequestCultureFeature>()?.RequestCulture.Culture;
    }
    
    public string GetString(string key, string? culture = null) => 
        _localizer[key, (culture ?? _currentCultureInfo?.Name) ?? string.Empty];


    public string GetString(string key, params object[] args) => 
        string.Format(_localizer[key], args);
    
    public string GetPluralString(string key, int count, string? culture = null)
    {
        var pluralKey = GetPluralKey(key, count, culture);
        return string.Format(_localizer[pluralKey], count);
    }

    public string GetPluralString(string key, int count, params object[] args)
    {
        var pluralKey = GetPluralKey(key, count);
        var allArgs = new object[] {count}.Concat(args).ToArray();
        return string.Format(_localizer[pluralKey], allArgs);
    }
    
    private string GetPluralKey(string key, int count, string? culture = null)
    {
        return _currentCultureInfo?.Name switch
        {
            "fr-FR" => count == 1 ? $"{key}.Singular" : $"{key}.Plural",
            "es-ES" => count == 1 ? $"{key}.Singular" : $"{key}.Plural",
            "en-US" => count == 1 ? $"{key}.Singular" : $"{key}.Plural",
            "de-DE" => count == 1 ? $"{key}.Singular" : $"{key}.Plural",
            "zh-CN" => count == 1 ? $"{key}.Singular" : $"{key}.Plural",
            "ja-JP" => count == 1 ? $"{key}.Singular" : $"{key}.Plural",
            "ru-RU" => GetRussianPluralKey(key, count),
            _ => count == 1 ? $"{key}.Singular" : $"{key}.Plural"
        };
    }
    
    private static string GetRussianPluralKey(string key, int count) 
    {
        var lastDigit = count % 10;
        var lastTwoDigits = count % 100;
        
        if(lastTwoDigits >= 11 && lastTwoDigits <= 14) 
            return $"{key}.Many";
        
        return lastDigit switch
        {
            1 => $"{key}.Singular",
            >= 2 and <= 4 => $"{key}.Few",
            _ => $"{key}.Many"
        };
    }
}