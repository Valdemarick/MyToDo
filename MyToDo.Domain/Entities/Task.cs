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
    private readonly List<Tag> _tags = new();

    private Task(
        string title,
        string description,
        TaskStatus status,
        Priority priority,
        TaskType taskType,
        Guid creatorId,
        Guid? executorId) : base(Guid.NewGuid())
    {
        Title = title;
        Description = description;
        Status = status;
        Priority = priority;
        TaskType = taskType;
        CreatorId = creatorId;
        ExecutorId = executorId;
    }

    protected Task()
    {
    }
    
    public string Title { get; private set; }

    public string Description { get; private set; }
    
    public TaskStatus Status { get; private set; }

    public Priority Priority { get; private set; }
    
    public TaskType TaskType { get; private set; }
    
    public DateTimeOffset CreatedOn { get; private set; }
    
    public DateTimeOffset? LastUpdatedOn { get; private set; }
    
    public DateTimeOffset? CompletedOn { get; private set; }

    public Member? Executor { get; private set; }
    public Guid? ExecutorId { get; private set; }
    
    public Member Creator { get; private set; }
    public Guid CreatorId { get; private set; }

    public IEnumerable<Comment> Comments => _comments;
    public IEnumerable<Tag> Tags => _tags;

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

    public void Assign(Member executor)
    {
        Executor = executor;
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public Result RemoveComment(Comment comment)
    {
        if (_comments.All(c => c.Id != comment.Id))
        {
            return Result.Failure(DomainErrors.Task.TaskDoesNotHaveThisComment);
        }

        _comments.Remove(comment);

        return Result.Success();
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

    public void SetCreatedOn(DateTimeOffset dateTimeOffset)
    {
        CreatedOn = dateTimeOffset;
    }

    public void SetLastUpdatedOn(DateTimeOffset dateTimeOffset)
    {
        LastUpdatedOn = dateTimeOffset;
    }

    public void SetCompletedOn(DateTimeOffset dateTimeOffset)
    {
        CompletedOn = dateTimeOffset;
    }
    
    internal static Task Create(
        string title,
        string description,
        Priority priority,
        TaskType taskType,
        Guid creatorId,
        Guid? executorId)
    {
        return new Task(
            title,
            description,
            TaskStatus.Open,
            priority,
            taskType,
            creatorId,
            executorId);
    }
}
