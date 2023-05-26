using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.ValueObjects.Common;

public abstract class BasePagedList<TEntity> where TEntity : AggregateRoot
{
    public required List<TEntity> Items { get; set; }

    public required int TotalCount { get; set; }
}
