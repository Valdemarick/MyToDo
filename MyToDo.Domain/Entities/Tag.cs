using MyToDo.Domain.Errors;
using MyToDo.Domain.Primitives;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Entities;

public sealed class Tag : AggregateRoot
{
    private readonly List<Task> _tasks = new();

    internal Tag(string name) : base(Guid.NewGuid())
    {
        Name = name;
    }

    private Tag()
    {
    }

    public string Name { get; private set; } = string.Empty;
    
    public IReadOnlyCollection<Task> Tasks => _tasks;
    
    public DateTimeOffset CreatedOn { get; private set; }
    
    public DateTimeOffset? LastUpdatedOn { get; private set; }

    public void SetCreatedOn(DateTimeOffset dateTimeOffset)
    {
        CreatedOn = dateTimeOffset;
    }

    public void SetLastUpdatedOn(DateTimeOffset dateTimeOffset)
    {
        LastUpdatedOn = dateTimeOffset;
    }

    public Result UpdateName(string? name)
    {
        if (name is null)
        {
            return Result.Failure(DomainErrors.Tag.TagNameValidationError);
        }

        Name = name;

        return Result.Success();
    }
}
