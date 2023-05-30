namespace MyToDo.HttpContracts.Members;

public sealed class UpdateMemberDto
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Guid RoleId { get; set; }

    public bool IsActive { get; set; }
}
