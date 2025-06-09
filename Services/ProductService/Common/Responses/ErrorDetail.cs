namespace ProductService.Common.Responses;

public class ErrorDetail
{
    public string? Field { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? StatusCode { get; set; }
}