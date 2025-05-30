namespace ProductService.Common.Exceptions;

public class ValidationException(List<ValidationError> validationErrors)
    : BaseException("Validation failed", "Validation_error")
{
    public List<ValidationError> ValidationErrors { get; } = validationErrors;
}