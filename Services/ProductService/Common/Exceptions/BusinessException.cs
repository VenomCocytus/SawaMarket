namespace ProductService.Common.Exceptions;

public class BusinessException(string message, string errorCode = "Business_error") : BaseException(message, errorCode);