using MyToDo.Domain.Abstractions;

namespace MyToDo.Infrastructure.DateTime;

public sealed class DateTimeOffsetProvider : IDateTimeOffsetProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
