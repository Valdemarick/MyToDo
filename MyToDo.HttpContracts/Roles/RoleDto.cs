namespace MyToDo.HttpContracts.Roles;

public sealed class RoleDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
}
