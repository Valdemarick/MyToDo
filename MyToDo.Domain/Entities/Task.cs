using MyToDo.Domain.Enums;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Primitives;
using MyToDo.Domain.Shared;
using TaskStatus = MyToDo.Domain.Enums.TaskStatus;

namespace MyToDo.Domain.Entities;

/// <summary>
/// Task entity.
/// </summary>
public sealed class Task : AggregateRoot
{
    private readonly List<Comment> _comments = new();

    private Task(
        Guid id,
        string title,
        string description,
        TaskStatus status,
        Priority priority,
        TaskType taskType,
        DateTime deadline,
        Guid creatorId,
        Guid? executorId) : base(id)
    {
        Title = title;
        Description = description;
        Status = status;
        Priority = priority;
        TaskType = taskType;
        Deadline = deadline;
        CreatorId = creatorId;
        ExecutorId = executorId;
        CreatedOn = DateTime.UtcNow;
    }
    
    public string Title { get; private set; }

    public string Description { get; private set; }
    
    public TaskStatus Status { get; private set; }

    public Priority Priority { get; private set; }
    
    public TaskType TaskType { get; private set; }
    
    public DateTime Deadline { get; private set; }

    public DateTime CreatedOn { get; private set; }
    
    public DateTime? LastUpdatedOn { get; private set; }
    
    public DateTime? CompletedOn { get; private set; }
    
    public Guid? ExecutorId { get; private set; }
    
    public Guid CreatorId { get; private set; }

    public Member Creator { get; private set; }
    
    public Member? Executor { get; private set; }

    public IReadOnlyCollection<Comment> Comments => _comments;

    public void Complete()
    {
        Status = TaskStatus.Completed;
    }

    public Result StartWorkOnTask()
    {
        if (Status == TaskStatus.Completed)
        {
            return Result.Failure(DomainErrors.Task.TaskAlreadyInProgress);
        }

        Status = TaskStatus.InProgress;
        
        return Result.Success();
    }

    public Result Reopen()
    {
        if (Status is TaskStatus.Open or TaskStatus.InProgress)
        {
            return Result.Failure(DomainErrors.Task.TaskNotCompleted);
        }

        Status = TaskStatus.Reopen;

        return Result.Success();
    }

    public static Task Create(
        Guid id,
        string title,
        string description,
        Priority priority,
        TaskType taskType,
        DateTime deadline,
        Guid creatorId,
        Guid? executorId)
    {
        return new Task(
            Guid.NewGuid(),
            title,
            description,
            TaskStatus.Open,
            priority,
            taskType,
            deadline,
            creatorId,
            executorId);
    }

    public void AssignTask(Member executor)
    {
        Executor = executor;
    }
}
