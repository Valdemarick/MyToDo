using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class Comment : BaseEntity
{
    public Comment(Guid id) 
        : base(id)
    {
    }

    public string Text { get; private set; }
    
    public DateTime CreatedOn { get; private set; }

    public DateTime? LastUpdatedOn { get; private set; }

    public Guid TaskId { get; private set; }
    
    public Guid ReviewerId { get; private set; }

    public Task Task { get; private set; }

    public Member Reviewer { get; private set; }
}
