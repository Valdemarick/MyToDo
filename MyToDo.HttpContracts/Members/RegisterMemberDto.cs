using System.ComponentModel.DataAnnotations;

namespace MyToDo.HttpContracts.Members;

public sealed class RegisterMemberDto
{
    [Required(ErrorMessage = "Это поле обязательное")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Это поле обязательное")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Это поле обязательное")]
    [EmailAddress(ErrorMessage = "Неверный формат")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Это поле обязательное")]
    [MinLength(4, ErrorMessage = "Минимальная длина пароля - 4 символа")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Это поле обязательное")]
    public Guid RoleId { get; set; }

    public bool IsActive { get; set; }
}
