using MediatR;

namespace ProductService.Application.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger _logger = logger;

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return next(cancellationToken);
        }
        catch (Exception exception)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError(exception, "Application Request: Unhandled Exception for Request {RequestName} {Request}", requestName, request);
            throw;
        }
    }
}