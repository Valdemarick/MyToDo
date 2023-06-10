using MyToDo.HttpContracts.Common;
using MyToDo.HttpContracts.Enums;

namespace MyToDo.HttpContracts.Tasks;

public sealed class TaskPageRequestDto : BasePageRequestDto
{
    public TaskTypeDto TaskType { get; set; } = TaskTypeDto.NotChosen;

    public TaskStatusDto TaskStatus { get; set; } = TaskStatusDto.NotChosen;

    public PriorityDto Priority { get; set; } = PriorityDto.NotChosen;
    
    public bool IsShowOnlyMyTask { get; set; }
}
