using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class Permission : AggregateRoot
{
    private List<Role> _roles = new();

    public Permission(string name) 
    : base(Guid.NewGuid())
    {
        Name = name;
    }

    private Permission()
    {
    }
    
    public string Name { get; private set; } = string.Empty;
    
    public IReadOnlyCollection<Role> Roles => _roles;
}
