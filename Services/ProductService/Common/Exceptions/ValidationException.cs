namespace ProductService.Common.Exceptions;

public class ValidationException(List<ValidationError> validationErrors)
    : BaseException("One or more validation failures have occurred.", "Validation_error")
{
    public List<ValidationError> ValidationErrors { get; } = validationErrors;
}