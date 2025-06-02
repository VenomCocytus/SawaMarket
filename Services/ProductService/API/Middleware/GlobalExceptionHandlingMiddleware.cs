using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.Extensions.Localization;
using ProductService.Common.Exceptions;
using ProductService.Common.Response;
using ValidationException = ProductService.Common.Exceptions.ValidationException;

namespace ProductService.API.Middleware;

public class GlobalExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlingMiddleware> logger,
    IStringLocalizer<GlobalExceptionHandlingMiddleware> localizer)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled exception occured. TraceId: {TracedId}", 
                context.TraceIdentifier);
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = exception switch
        {
            ValidationException validationException => new ErrorResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = localizer["Validation.Failed"],
                Details = validationException.ValidationErrors.Select(e => new ErrorDetail
                {
                    Field = e.PropertyName,
                    Message = localizer[e.ErrorMessage] ?? e.ErrorMessage,
                    StatusCode = e.ErrorCode,
                }).ToList(),
                TraceId = context.TraceIdentifier,
            },

            BusinessException businessException => new ErrorResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = localizer[businessException.Message] ?? businessException.Message,
                Code = businessException.ErrorCode,
                TraceId = context.TraceIdentifier,
            },

            UnauthorizedAccessException => new ErrorResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status401Unauthorized,
                Message = localizer["Error.Unauthorized"],
                Code = "UNAUTHORIZED",
                TraceId = context.TraceIdentifier,
            },

            _ => new ErrorResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = localizer["Error.InternalServerError"],
                Code = "INTERNAL_SERVER_ERROR",
                TraceId = context.TraceIdentifier,
            }
        };
        response.StatusCode = errorResponse.StatusCode;
        
        await response.WriteAsync(JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        }));
    }
}