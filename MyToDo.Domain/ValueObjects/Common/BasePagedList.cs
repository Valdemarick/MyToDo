using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.ValueObjects.Common;

public abstract class BasePagedList<TEntity> where TEntity : AggregateRoot
{
    protected BasePagedList(List<TEntity> items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }
    
    public List<TEntity> Items { get; set; }

    public int TotalCount { get; set; }
}
