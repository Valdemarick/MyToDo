namespace MyToDo.Application.Common.Dtos.Members;

public sealed class MemberDto
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public bool IsActive { get; set; }
}
