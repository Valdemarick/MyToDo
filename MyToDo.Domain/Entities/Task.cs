using Microsoft.Extensions.Internal;
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
    private readonly List<Comment> _comments = new();

    private Task(
        Guid id,
        string title,
        string description,
        TaskStatus status,
        Priority priority,
        TaskType taskType,
        DateTimeOffset deadline,
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
    
    public DateTimeOffset Deadline { get; private set; }

    public DateTimeOffset CreatedOn { get; private set; }
    
    public DateTimeOffset? LastUpdatedOn { get; private set; }
    
    public DateTimeOffset? CompletedOn { get; private set; }
    
    public Guid? ExecutorId { get; private set; }
    
    public Guid CreatorId { get; private set; }

    public Member Creator { get; private set; }
    
    public Member? Executor { get; private set; }

    public IReadOnlyCollection<Comment> Comments => _comments;

    public Result Complete(IDateTimeOffsetProvider dateTimeOffsetProvider)
    {
        if (Status is TaskStatus.Completed)
        {
            return Result.Failure(DomainErrors.Task.TaskIsAlreadyCompleted);
        }
        
        Status = TaskStatus.Completed;
        CompletedOn = dateTimeOffsetProvider.UtcNow;

        return Result.Success();
    }

    public Result StartWorkOnTask(IDateTimeOffsetProvider dateTimeOffsetProvider)
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
        LastUpdatedOn = dateTimeOffsetProvider.UtcNow;
        
        return Result.Success();
    }

    public Result Reopen(IDateTimeOffsetProvider dateTimeOffsetProvider)
    {
        if (Status is TaskStatus.Open or TaskStatus.InProgress)
        {
            return Result.Failure(DomainErrors.Task.TaskIsNotCompleted);
        }

        Status = TaskStatus.Reopen;
        LastUpdatedOn = dateTimeOffsetProvider.UtcNow;

        return Result.Success();
    }

    public static Task Create(
        Guid id,
        string title,
        string description,
        Priority priority,
        TaskType taskType,
        DateTimeOffset deadline,
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

    public void Assign(Member executor, IDateTimeOffsetProvider dateTimeOffsetProvider)
    {
        Executor = executor;
        LastUpdatedOn = dateTimeOffsetProvider.UtcNow;
    }

    public void AddComment(Comment comment, IDateTimeOffsetProvider dateTimeOffsetProvider)
    {
        _comments.Add(comment);
        LastUpdatedOn = dateTimeOffsetProvider.UtcNow;
    }

    public Result RemoveComment(Comment comment, IDateTimeOffsetProvider dateTimeOffsetProvider)
    {
        if (_comments.All(c => c.Id != comment.Id))
        {
            return Result.Failure(DomainErrors.Task.TaskDoesNotHaveThisComment);
        }

        _comments.Remove(comment);
        LastUpdatedOn = dateTimeOffsetProvider.UtcNow;

        return Result.Success();
    }

    public void UpdateDescription(string newDescription, IDateTimeOffsetProvider dateTimeOffsetProvider)
    {
        Description = newDescription;
        LastUpdatedOn = dateTimeOffsetProvider.UtcNow;
    }

    public void UpdateTitle(string newTitle, IDateTimeOffsetProvider dateTimeOffsetProvider)
    {
        Title = newTitle;
        LastUpdatedOn = dateTimeOffsetProvider.UtcNow;
    }
}
