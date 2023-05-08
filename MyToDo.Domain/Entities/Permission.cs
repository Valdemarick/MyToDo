using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class Permission : AggregateRoot
{
    public Permission(string name) : base(Guid.NewGuid())
    {
        Name = name;
    }

    private Permission()
    {
    }
    
    public string Name { get; private set; }
    
    public ICollection<Role> Roles { get; private set; }
}
