namespace ProductService.Application.Interfaces;

public interface ILocalizationService
{
    string GetString(string key, string? culture = null);
    string GetString(string key, params object[] args);
    string GetPluralString(string key, int count, string? culture = null);
    string GetPluralString(string key, int count, params object[] args);
}