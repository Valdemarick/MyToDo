using System.ComponentModel.DataAnnotations;

namespace MyToDo.HttpContracts.Tags;

public sealed record UpdateTagDto
{
    [Required(ErrorMessage = "Это поле обязательное")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Это поле обязательное")]
    public string Name { get; set; }
}
