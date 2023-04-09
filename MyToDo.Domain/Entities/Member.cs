using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class Member : AggregateRoot
{
    public Member(Guid id) : base(id)
    {
    }
}
