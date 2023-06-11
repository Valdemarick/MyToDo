namespace MyToDo.Domain.Abstractions;

public interface IDateTimeService
{
    DateTime UtcNow { get; }
}
