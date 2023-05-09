using MyToDo.Domain.Abstractions;

namespace MyToDo.Infrastructure.Services;

internal sealed class DateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow;
}
