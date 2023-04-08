namespace MyToDo.Domain.Enums;

/// <summary>
/// Task statuses.
/// </summary>
public enum TaskStatus
{
    /// <summary>
    /// Task is open and waiting to be executed.
    /// </summary>
    Open = 0,
    
    /// <summary>
    /// Task is executing.
    /// </summary>
    InProgress = 1,
    
    /// <summary>
    /// Task is completed.
    /// </summary>
    Completed = 2,
}
