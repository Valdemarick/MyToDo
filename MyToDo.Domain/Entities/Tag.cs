using MyToDo.Domain.Errors;
using MyToDo.Domain.Primitives;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Entities;

public sealed class Tag : AggregateRoot
{
    internal Tag(string name) : base(Guid.NewGuid())
    {
        Name = name;

        Tasks = new List<Task>();
    }

    private Tag()
    {
        Tasks = new List<Task>();
    }

    public string Name { get; private set; }
    
    public IEnumerable<Task> Tasks { get; }
    
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
            return Result.Failure(DomainErrors.Tag.TagNameIsAlreadyOccupied);
        }

        Name = name;

        return Result.Success();
    }
}
