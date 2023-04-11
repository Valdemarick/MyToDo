using MyToDo.Domain.Abstractions;

namespace MyToDo.Infrastructure.DateTime;

internal sealed class DateTimeOffsetProvider : IDateTimeOffsetProvider
{
    public DateTimeOffset UtcNow { get; } = DateTimeOffset.UtcNow;
}
