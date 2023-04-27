using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class Member : AggregateRoot
{
    private Member(Guid id) : base(id)
    {
    }

    public string Name { get; private set; }
    
    public string Surname { get; private set; }

    public string FullName => $"{Surname} {Name}";
    
    public DateTimeOffset RegisteredOn { get; private set; }

    public static Member Create(Guid id)
    {
        return new Member(id);
    }
}
