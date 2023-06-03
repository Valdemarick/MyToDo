using MyToDo.Domain.Abstractions;
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
    private readonly List<Tag> _tags = new();

    internal Task(
        string title,
        string description,
        TaskStatus status,
        Priority priority,
        TaskType taskType,
        DateTime deadLine,
        TaskCreator creator,
        TaskExecutor? executor) : base(Guid.NewGuid())
    {
        Title = title;
        Description = description;
        Status = status;
        Priority = priority;
        TaskType = taskType;
        DeadLine = deadLine;
        Creator = creator;
        Executor = executor;
    }

    private Task()
    {
    }
    
    public string Title { get; private set; }

    public string Description { get; private set; }
    
    public TaskStatus Status { get; private set; }

    public Priority Priority { get; private set; }
    
    public TaskType TaskType { get; private set; }
    
    public DateTime CreatedOn { get; private set; }
    
    public DateTime DeadLine { get; private set; }
    
    public DateTime? LastUpdatedOn { get; private set; }
    
    public DateTime? CompletedOn { get; private set; }

    public TaskExecutor? Executor { get; private set; }
    public Guid? ExecutorId { get; private set; }
    
    public TaskCreator Creator { get; private set; }
    public Guid CreatorId { get; private set; }
    
    public IEnumerable<Tag> Tags => _tags;

    public Result Complete()
    {
        if (Status is TaskStatus.Completed)
        {
            return Result.Failure(DomainErrors.Task.TaskIsAlreadyCompleted);
        }
        
        Status = TaskStatus.Completed;

        return Result.Success();
    }

    public Result StartWorkingOnTask()
    {
        if (Status is TaskStatus.InProgress)
        {
            return Result.Failure(DomainErrors.Task.TaskIsAlreadyInProgress);
        }

        if (Status is TaskStatus.Completed)
        {
            return Result.Failure(DomainErrors.Task.TaskIsCompleted);
        }

        Status = TaskStatus.InProgress;

        return Result.Success();
    }

    public Result Reopen()
    {
        if (Status is TaskStatus.Open or TaskStatus.InProgress)
        {
            return Result.Failure(DomainErrors.Task.TaskIsNotCompleted);
        }

        Status = TaskStatus.Reopen;
        CompletedOn = null;

        return Result.Success();
    }

    public void Assign(TaskExecutor executor)
    {
        Executor = executor;
    }

    public void UpdateDescription(string newDescription)
    {
        Description = newDescription;
    }

    public void UpdateTitle(string newTitle)
    {
        Title = newTitle;
    }

    public void Close()
    {
        Status = TaskStatus.Completed;
    }

    public void SetCreatedOn(DateTime dateTime)
    {
        CreatedOn = dateTime;
    }

    public void SetLastUpdatedOn(DateTime dateTime)
    {
        LastUpdatedOn = dateTime;
    }

    public void SetCompletedOn(DateTime dateTime)
    {
        CompletedOn = dateTime;
    }

    public Result AddTag(Tag tag)
    {
        if (_tags.Contains(tag))
        {
            return Result.Failure(DomainErrors.Task.TaskAlreadyHaveThisTag);
        }
        
        _tags.Add(tag);

        return Result.Success();
    }

    public Result RemoveTag(Tag tag)
    {
        if (!_tags.Contains(tag))
        {
            return Result.Failure(DomainErrors.Task.TaskDoesNotContainThisTag);
        }

        _tags.Remove(tag);

        return Result.Success();
    }
}
