using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class TaskExecutor : BaseEntity
{
    internal TaskExecutor(
        string fullName,
        Guid memberId) : base(Guid.NewGuid())
    {
        FullName = fullName;
        MemberId = memberId;
    }

    private TaskExecutor()
    {
    }

    public Task Task { get; private set; }

    public Guid MemberId { get; private set; }
    
    public string FullName { get; private set; }
}
