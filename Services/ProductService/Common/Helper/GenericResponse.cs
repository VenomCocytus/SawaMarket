using Lombok.NET;

namespace ProductService.Common.Helper;

[NoArgsConstructor]
[AllArgsConstructor]
public partial class GenericResponse<T>
{
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public string? Message { get; private set; }
    public List<string> ValidationErrors { get; private set; } = [];
    public string? ErrorCode { get; private set; }
    public DateTimeOffset Timestamp { get; private set; }
    
    private static DateTimeOffset GetCurrentTimestamp() => DateTimeOffset.UtcNow;

    public static GenericResponse<T> Empty() => new()
    {
        Timestamp = GetCurrentTimestamp(),
        IsSuccess = false,
        Message = string.Empty
    };

    public static GenericResponse<T> Success(T data) => new()
    {
        Timestamp = GetCurrentTimestamp(),
        IsSuccess = true,
        Message = "Operation.successfully",
        Data = data
    };
    
    public static GenericResponse<T> Success(string successMessage) => new()
    {
        Timestamp = GetCurrentTimestamp(),
        IsSuccess = true,
        Message = successMessage
    };
    
    public static GenericResponse<T> Success(T data, string successMessage) => new()
    {
        Timestamp = GetCurrentTimestamp(),
        IsSuccess = true,
        Message = successMessage,
        Data = data
    };
    
    public static GenericResponse<T> Failure(string? errorCode = null) => new()
    {
        Timestamp = GetCurrentTimestamp(),
        IsSuccess = false,
        Message = "Operation.Failure",
        ErrorCode = errorCode
    };
    
    public static GenericResponse<T> Failure(string errorMessage, 
        string? errorCode = null) => new()
    {
        Timestamp = GetCurrentTimestamp(),
        IsSuccess = false,
        Message = errorMessage,
        ErrorCode = errorCode
    };
    
    public static GenericResponse<T> ValidationFailure(List<string> validationErrors) => new()
    {
        Timestamp = GetCurrentTimestamp(),
        IsSuccess = false,
        ValidationErrors = validationErrors
    };
    
    public static GenericResponse<T> ValidationFailure(List<string> validationErrors, 
        string errorMessage, string? errorCode) => new()
    {
        Timestamp = GetCurrentTimestamp(),
        IsSuccess = false,
        Message = errorMessage,
        ErrorCode = errorCode,
        ValidationErrors = validationErrors
    };
}