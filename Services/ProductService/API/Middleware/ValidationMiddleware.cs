using Microsoft.AspNetCore.Mvc.Filters;
using ProductService.Application.Constants;
using ProductService.Application.Exceptions;
using ProductService.Contract.Common;
using ValidationException = FluentValidation.ValidationException;

namespace ProductService.API.Middleware;

public class ValidationMiddleware : IAsyncActionFilter
{
    // Base validation exception class
    public async Task OnActionExecutionAsyncBaseValidationClass(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errorsInModelStateList = context.ModelState
                .Where(e => e.Value is { Errors.Count: > 0 })
                .Select(e => new
                {
                    Field = e.Key,
                    Errors = e.Value?.Errors.Select(error => error.ErrorMessage).ToArray()
                })
                .ToList();

            if (errorsInModelStateList.Count != 0)
            {
                var errorLines = errorsInModelStateList
                    .Select(e => $"{e.Field}: {string.Join(", ", e.Errors ?? [])}")
                    .Distinct()
                    .Aggregate((previous, nextLine) => previous + "\n" + nextLine);

                throw new ValidationException(errorLines);
            }
        }

        await next();
    }

    // Custom validation exception class
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var validationErrorList = context.ModelState
                .Where(e => e.Value is { Errors.Count: > 0 })
                .SelectMany(e =>
                {
                    if (e.Value != null)
                        return e.Value.Errors.Select(error => new ValidationError(
                            ErrorCode.ValidationError,
                            e.Key,
                            error.ErrorMessage,
                            e.Value.AttemptedValue
                        ));
                    
                    return [];
                })
                .ToList();

            if (validationErrorList.Count != 0)
                throw new Application.Exceptions.ValidationException(validationErrorList);
        }

        await next();
    }
}