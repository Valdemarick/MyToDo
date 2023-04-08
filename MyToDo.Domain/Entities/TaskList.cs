namespace MyToDo.Domain.Entities;

/// <summary>
/// Task list entity.
/// </summary>
public class TaskList : BaseEntity
{
    /// <summary>
    /// Task list title.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Tasks linked to this list.
    /// </summary>
    public ICollection<Task> Tasks { get; set; } = null!;
}
