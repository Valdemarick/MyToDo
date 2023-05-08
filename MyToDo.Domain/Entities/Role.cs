using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class Role : AggregateRoot
{
    public Role(string name) : base(Guid.NewGuid())
    {
        Name = name;
    }

    private Role()
    {
    }
    
    public string Name { get; private set; }
    
    public ICollection<Member> Members { get; private set; }
    
    public ICollection<Permission> Permissions { get; private set; }
}
