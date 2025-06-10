namespace ProductService.Application.Exceptions;

public abstract class BaseException(string message, string errorCode, Exception? innerException = null)
    : Exception(message, innerException)
{
    public string ErrorCode { get; } = errorCode;
    public Dictionary<string, object> AdditionalData { get; } = new();
}