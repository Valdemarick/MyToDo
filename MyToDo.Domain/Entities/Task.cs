using TaskStatus = MyToDo.Domain.Enums.TaskStatus;

namespace MyToDo.Domain.Entities;

/// <summary>
/// Task entity.
/// </summary>
public sealed class Task : BaseEntity
{
    /// <summary>
    /// Task title.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Task status. 
    /// </summary>
    public TaskStatus Status { get; set; }
    
    /// <summary>
    /// Task deadline.
    /// </summary>
    public DateTimeOffset Deadline { get; set; }
    
    /// <summary>
    /// Executor identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// List identifier, the task is linked to.
    /// </summary>
    public Guid ListId { get; set; }

    /// <summary>
    /// The task's executor.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// The task list this task linked to.
    /// </summary>
    public TaskList TaskList { get; set; } = null!;
}
