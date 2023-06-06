using System.ComponentModel.DataAnnotations;
using MyToDo.HttpContracts.Enums;

namespace MyToDo.HttpContracts.Tasks;

public sealed class CreateTaskDto
{
    [Required(ErrorMessage = "Это поле обязательное")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Это поле обязательное")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Это поле обязательное")]
    public Guid? ExecutorId { get; set; }
    
    [Required(ErrorMessage = "Это поле обязательное")]
    public TaskTypeDto TaskType { get; set; }
    
    [Required(ErrorMessage = "Это поле обязательное")]
    public PriorityDto Priority { get; set; }
    
    [Required(ErrorMessage = "Это поле обязательное")]
    public DateTime Deadline { get; set; }
}
