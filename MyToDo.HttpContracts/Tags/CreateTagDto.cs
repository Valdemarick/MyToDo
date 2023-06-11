using System.ComponentModel.DataAnnotations;

namespace MyToDo.HttpContracts.Tags;

public sealed class CreateTagDto
{
    [Required(ErrorMessage = "Это поле обязательное")]
    public string Name { get; set; }
}