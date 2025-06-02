namespace ProductService.Common.Helper;

public class GenericResponse<T>
{
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public string? ErrorMessage { get; private set; }
    public List<string> ValidationErrors { get; private set; } = new();
    public string? ErrorCode { get; private set; }
    
    public GenericResponse() {}

    public static GenericResponse<T> Success(T data) => new()
    {
        IsSuccess = true,
        Data = data
    };
    
    public static GenericResponse<T> Failure(string errorMessage, string? errorCode = null) => new()
    {
        IsSuccess = false,
        ErrorMessage = errorMessage,
        ErrorCode = errorCode
    };
    
    public static GenericResponse<T> ValidationFailure(List<string> validationErrors) => new()
    {
        IsSuccess = false,
        ValidationErrors = validationErrors
    };
}