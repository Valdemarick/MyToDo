using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class TaskCreator : BaseEntity
{
    internal TaskCreator(
        string fullName,
        Guid memberId) : base(Guid.NewGuid())
    {
        FullName = fullName;
        MemberId = memberId;
    }

    private TaskCreator()
    {
    }

    public Task Task { get; private set; }

    public Guid MemberId { get; private set; }
    
    public string FullName { get; private set; }
}
