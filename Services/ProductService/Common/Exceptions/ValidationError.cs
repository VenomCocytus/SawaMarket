namespace ProductService.Common.Exceptions;

public record ValidationError(
    string PropertyName,
    string ErrorMessage,
    object? Attempted
) { }