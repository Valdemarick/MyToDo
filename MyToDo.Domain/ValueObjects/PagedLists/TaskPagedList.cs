using MyToDo.Domain.ValueObjects.Common;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Domain.ValueObjects.PagedLists;

public sealed class TaskPagedList : BasePagedList<Task>
{
    public TaskPagedList(List<Task> items, int totalCount) : base(items, totalCount)
    {
    }
}
