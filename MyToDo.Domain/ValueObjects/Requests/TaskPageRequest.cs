using MyToDo.Domain.Entities;
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
        Priority priority,
        List<Tag> tags,
        Guid? executorId = null) 
        : base(searchString, pageIndex, pageSize)
    {
        TaskStatus = taskStatus;
        TaskType = taskType;
        Priority = priority;
        ExecutorId = executorId;
        Tags = tags;
    }

    public TaskStatus TaskStatus { get; set; }

    public Priority Priority { get; set; }

    public TaskType TaskType { get; set; }

    public Guid? ExecutorId { get; set; }

    public List<Tag>? Tags { get; set; }
}
