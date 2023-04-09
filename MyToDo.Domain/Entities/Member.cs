using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class Member : AggregateRoot
{
    private Member(Guid id) : base(id)
    {
    }

    public static Member Create(Guid id)
    {
        return new Member(id);
    }
}
