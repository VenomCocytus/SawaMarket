namespace ProductService.Common.Exceptions;

public record ValidationError(
    string ErrorCode,
    string PropertyName,
    string ErrorMessage,
    object? Attempted
) { }