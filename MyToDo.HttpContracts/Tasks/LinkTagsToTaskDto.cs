using System.ComponentModel.DataAnnotations;

namespace MyToDo.HttpContracts.Tasks;

public sealed class LinkTagsToTaskDto
{
    [Required(ErrorMessage = "Это поле обязательное")]
    public Guid TaskId { get; set; }
    
    [Required(ErrorMessage = "Это поле обязательное")]
    public List<Guid> TagIds { get; set; } = new();
}
