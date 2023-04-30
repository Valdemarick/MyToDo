namespace MyToDo.Domain.Enums;

/// <summary>
/// Task statuses.
/// </summary>
public enum TaskStatus
{
    Unknown = 0,
    
    Open = 1,
    
    InProgress = 2,
    
    Completed = 3,
    
    Reopen = 4,
}
