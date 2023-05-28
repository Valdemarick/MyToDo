using MyToDo.Domain.Entities;
using MyToDo.Domain.ValueObjects.Common;

namespace MyToDo.Domain.ValueObjects.PagedLists;

public sealed class TagPagedList : BasePagedList<Tag>
{
    public TagPagedList(List<Tag> items, int totalCount) : base(items, totalCount)
    {
    }
}
