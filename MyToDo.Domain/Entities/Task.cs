using MyToDo.Domain.Primitives;
using TaskStatus = MyToDo.Domain.Enums.TaskStatus;

namespace MyToDo.Domain.Entities;

/// <summary>
/// Task entity.
/// </summary>
public sealed class Task : Entity
{
    public Task(
        Guid id,
        string title,
        TaskStatus status,
        DateTime deadline,
        Guid userId,
        Guid listId) : base(id)
    {
        Title = title;
        Status = status;
        Deadline = deadline;
        UserId = userId;
        ListId = listId;
        CreatedOn = DateTime.UtcNow;
    }
    
    public string Title { get; private set; } = null!;
    
    public TaskStatus Status { get; private set; }
    
    public DateTime Deadline { get; private set; }

    public DateTime CreatedOn { get; private set; }
    
    public DateTime? LastUpdatedOn { get; private set; }
    
    public DateTime? ClosedOn { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public Guid ListId { get; private set; }
    
    public User User { get; private set; } = null!;
    
    public TaskList TaskList { get; private set; } = null!;
}
