namespace MyToDo.Domain.Abstractions;

public interface IDateTimeService
{
    public DateTime UtcNow { get; }
}