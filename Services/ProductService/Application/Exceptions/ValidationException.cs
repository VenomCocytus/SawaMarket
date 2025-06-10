using ProductService.Contract.Common;

namespace ProductService.Application.Exceptions;

public class ValidationException(List<ValidationError> validationErrors)
    : BaseException("One or more validation failures have occurred.", "Validation_error")
{
    public List<ValidationError> ValidationErrors { get; } = validationErrors;
}