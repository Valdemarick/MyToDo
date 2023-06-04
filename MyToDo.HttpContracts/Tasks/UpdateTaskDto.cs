using MyToDo.HttpContracts.Enums;

namespace MyToDo.HttpContracts.Tasks;

public class UpdateTaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid? ExecutorId { get; set; }
    public TaskTypeDto TaskType { get; set; }
    public PriorityDto Priority { get; set; }
    public DateTime Deadline { get; set; }
}