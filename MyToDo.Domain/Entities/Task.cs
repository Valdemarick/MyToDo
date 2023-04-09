using MyToDo.Domain.Enums;
using MyToDo.Domain.Primitives;
using TaskStatus = MyToDo.Domain.Enums.TaskStatus;

namespace MyToDo.Domain.Entities;

/// <summary>
/// Task entity.
/// </summary>
public sealed class Task : AggregateRoot
{
    private Task(
        Guid id,
        string title,
        string description,
        TaskStatus status,
        Priority priority,
        DateTime deadline,
        Guid creatorId,
        Guid? executorId) : base(id)
    {
        Title = title;
        Description = description;
        Status = status;
        Priority = priority;
        Deadline = deadline;
        CreatorId = creatorId;
        ExecutorId = executorId;
        CreatedOn = DateTime.UtcNow;
    }
    
    public string Title { get; private set; }

    public string Description { get; private set; }
    
    public TaskStatus Status { get; private set; }

    public Priority Priority { get; private set; }
    
    public DateTime Deadline { get; private set; }

    public DateTime CreatedOn { get; private set; }
    
    public DateTime? LastUpdatedOn { get; private set; }
    
    public DateTime? CompletedOn { get; private set; }
    
    public Guid? ExecutorId { get; private set; }
    
    public Guid CreatorId { get; private set; }

    public Member Creator { get; private set; }
    
    public Member? Executor { get; private set; }
}
