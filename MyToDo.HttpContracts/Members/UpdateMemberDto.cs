using System.ComponentModel.DataAnnotations;

namespace MyToDo.HttpContracts.Members;

public sealed class UpdateMemberDto
{
    [Required(ErrorMessage = "Это поле обязательное")]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Это поле обязательное")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Это поле обязательное")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Это поле обязательное")]
    [EmailAddress(ErrorMessage = "Неверный формат")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Это поле обязательное")]
    public Guid RoleId { get; set; }

    public bool IsActive { get; set; }
}
