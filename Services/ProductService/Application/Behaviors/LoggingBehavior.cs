using System.Diagnostics;
using MediatR;

namespace ProductService.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var requestId = Guid.NewGuid();

        logger.LogInformation("Handling {RequestName} with ID {RequestId}", requestName, requestId);

        var stopwatch = Stopwatch.StartNew();
        try
        {
            var response = await next(cancellationToken);
            stopwatch.Stop();

            logger.LogInformation("Handled {RequestName} with ID {RequestId} in {ElapsedMilliseconds} ms",
                requestName, requestId, stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (Exception exception)
        {
            stopwatch.Stop();
            logger.LogError(exception, "Error handling {RequestName} with ID {RequestId} after {ElapsedMilliseconds} ms",
                requestName, requestId, stopwatch.ElapsedMilliseconds);
            Console.WriteLine(exception);
            throw;
        }
}
}