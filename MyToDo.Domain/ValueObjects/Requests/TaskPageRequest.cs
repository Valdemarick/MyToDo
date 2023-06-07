using MyToDo.Domain.Enums;
using MyToDo.Domain.ValueObjects.Common;
using TaskStatus = MyToDo.Domain.Enums.TaskStatus;

namespace MyToDo.Domain.ValueObjects.Requests;

public sealed class TaskPageRequest : BasePageRequest
{
    public TaskPageRequest(
        string? searchString, 
        int pageIndex, 
        int pageSize,
        TaskStatus taskStatus,
        TaskType taskType,
        Priority priority) 
        : base(searchString, pageIndex, pageSize)
    {
        TaskStatus = taskStatus;
        TaskType = taskType;
        Priority = priority;
    }

    public TaskStatus TaskStatus { get; set; }

    public Priority Priority { get; set; }

    public TaskType TaskType { get; set; }
}
