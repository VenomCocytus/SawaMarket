using FluentValidation;
using MediatR;
using ProductService.Application.Exceptions;
using ProductService.Contract.Common;
using ValidationException = FluentValidation.ValidationException;

namespace ProductService.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any()) return await next(cancellationToken);
        
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task
            .WhenAll(validators
            .Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults
            .SelectMany(r => r.Errors).Where(f => f != null).ToList();

        if (failures.Count == 0) return await next(cancellationToken);
        {
            var validationErrors = failures.Select(
                f => new ValidationError(f.ErrorCode, f.PropertyName, f.ErrorMessage, f.AttemptedValue)).ToString();
            throw new ValidationException(validationErrors);
        }

    }
}