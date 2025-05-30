using ProductService.Common.Application.Interfaces;

namespace ProductService.Common.Application.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime Now => DateTime.Now;
}