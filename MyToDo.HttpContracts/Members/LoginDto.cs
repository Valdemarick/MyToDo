using System.ComponentModel.DataAnnotations;

namespace MyToDo.HttpContracts.Members;

public class LoginDto
{
    [Required(ErrorMessage = "Это поле обязательное")]
    [EmailAddress(ErrorMessage = "Неверный формат")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Это поле обязательное")]
    [MinLength(4, ErrorMessage = "Минимальная длина пароль - 4 символа")]
    public string Password { get; set; }
}