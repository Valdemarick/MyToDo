using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class TaskExecutor : BaseEntity
{
    private TaskExecutor(
        string fullName,
        Guid memberId) : base(Guid.NewGuid())
    {
        FullName = fullName;
        MemberId = memberId;
    }

    public Task Task { get; private set; }

    public Guid MemberId { get; private set; }
    
    public string FullName { get; private set; }

    internal static TaskExecutor Create(string fullName, Guid memberId)
    {
        return new TaskExecutor(fullName, memberId);
    }
}
