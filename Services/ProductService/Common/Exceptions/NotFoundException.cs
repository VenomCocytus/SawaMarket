namespace ProductService.Common.Exceptions;

public class NotFoundException(string name, object key)
    : ApplicationException($"Entity \"{name}\" ({key}) was not found.");