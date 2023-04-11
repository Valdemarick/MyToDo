namespace MyToDo.Domain.Abstractions;

public interface IDateTimeOffsetProvider
{
    public DateTimeOffset UtcNow { get; }
}
