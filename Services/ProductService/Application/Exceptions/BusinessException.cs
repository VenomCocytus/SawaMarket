namespace ProductService.Application.Exceptions;

public class BusinessException(string message, string errorCode = "Business_error") : BaseException(message, errorCode);