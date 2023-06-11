using MyToDo.HttpContracts.Enums;

namespace MyToDo.HttpContracts.Tasks;

public sealed class TaskFullInfoDto
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;
    
    public TaskStatusDto Status { get; set; }

    public PriorityDto Priority { get; set; }
    
    public TaskTypeDto TaskType { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public DateTime Deadline { get; set; }
    
    public DateTime? LastUpdatedOn { get; set; }
    
    public DateTime? CompletedOn { get; set; }

    public Guid CreatorId { get; set; }

    public Guid? ExecutorId { get; set; }
    
    public string CreatorFullName { get; set; } = null!;

    public string? ExecutorFullName { get; set; }
}
