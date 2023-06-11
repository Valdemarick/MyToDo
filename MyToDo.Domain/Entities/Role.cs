using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class Role : AggregateRoot
{
    private readonly List<Member> _members = new();
    private readonly List<Permission> _permission = new();

    public Role(string name) : base(Guid.NewGuid())
    {
        Name = name;
    }

    private Role()
    {
    }
    
    public string Name { get; private set; } = string.Empty;
    
    public IReadOnlyCollection<Member> Members => _members;
    
    public IReadOnlyCollection<Permission> Permissions => _permission;
}
