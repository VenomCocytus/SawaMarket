namespace ProductService.Common.Responses;

/// <summary>
/// Represents a generic response wrapper for API operations.
/// </summary>
/// <typeparam name="T">The type of the data returned in the response.</typeparam>
public class GenericResponse<T>
{
    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public string? Message { get; init; }
    public IReadOnlyList<string> ValidationErrors { get; init; } = [];
    public string? ErrorCode { get; init; }
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;

    
    /// <summary>
    /// Creates an empty failure response.
    /// </summary>
    public static GenericResponse<T> Empty() => new()
    {
        IsSuccess = false,
        Message = string.Empty
    };

    /// <summary>
    /// Creates a success response with data and default message.
    /// </summary>
    public static GenericResponse<T> Success(T data) => new()
    {
        IsSuccess = true,
        Message = "Operation.successfully",
        Data = data
    };
    
    /// <summary>
    /// Creates a success response with a custom message.
    /// </summary>
    public static GenericResponse<T> Success(string successMessage) => new()
    {
        IsSuccess = true,
        Message = successMessage
    };
    
    /// <summary>
    /// Creates a success response with data and a custom message.
    /// </summary>
    public static GenericResponse<T> Success(T data, string successMessage) => new()
    {
        IsSuccess = true,
        Message = successMessage,
        Data = data
    };
    
    /// <summary>
    /// Creates a failure response with an optional error code.
    /// </summary>
    public static GenericResponse<T> Failure(string? errorCode = null) => new()
    {
        IsSuccess = false,
        Message = "Operation.Failure",
        ErrorCode = errorCode
    };
    
    /// <summary>
    /// Creates a failure response with a custom error message and optional error code.
    /// </summary>
    public static GenericResponse<T> Failure(string errorMessage, 
        string? errorCode = null) => new()
    {
        IsSuccess = false,
        Message = errorMessage,
        ErrorCode = errorCode
    };
    
    /// <summary>
    /// Creates a validation failure response with error details.
    /// </summary>
    public static GenericResponse<T> ValidationFailure(IEnumerable<string> validationErrors) => new()
    {
        IsSuccess = false,
        ValidationErrors = validationErrors.ToArray()
    };
    
    /// <summary>
    /// Creates a validation failure response with error details, custom message, and error code.
    /// </summary>
    public static GenericResponse<T> ValidationFailure(IEnumerable<string> validationErrors, 
        string errorMessage, string? errorCode) => new()
    {
        IsSuccess = false,
        Message = errorMessage,
        ErrorCode = errorCode,
        ValidationErrors = validationErrors.ToArray()
    };
}