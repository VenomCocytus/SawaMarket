namespace ProductService.Common.Response;

public class ErrorResponse
{
    public bool Success { get; set; }
    public int StatusCode { get; init; }
    public string Message { get; set; } = string.Empty;
    public string? Code { get; set; }
    public List<ErrorDetail> Details { get; set; } = [];
    public string? TraceId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}