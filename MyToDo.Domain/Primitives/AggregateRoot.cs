namespace MyToDo.Domain.Primitives;

public abstract class AggregateRoot : BaseEntity
{
    protected AggregateRoot(Guid id) 
        : base(id)
    {
    }

    protected AggregateRoot()
    {
    }
}
