using System.ComponentModel.DataAnnotations;

namespace MyToDo.HttpContracts.Members;

public sealed record UpdateMemberActivityDto
{
    [Required(ErrorMessage = "Это поле обязательное")]
    public Guid MemberId { get; set; }

    public bool IsActive { get; set; }
}
