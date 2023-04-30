using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class Tag : AggregateRoot
{
    private Tag(string name) : base(Guid.NewGuid())
    {
        Name = name;

        Tasks = new List<Task>();
    }

    protected Tag()
    {
        Tasks = new List<Task>();
    }

    public string Name { get; }
    
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

    internal static Tag Create(string name)
    {
        return new Tag(name);
    }
}
