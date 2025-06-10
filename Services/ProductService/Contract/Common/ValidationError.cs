namespace ProductService.Contract.Common;

public record ValidationError(
    string ErrorCode,
    string PropertyName,
    string ErrorMessage,
    object? Attempted
) { }