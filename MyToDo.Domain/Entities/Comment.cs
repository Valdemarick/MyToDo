using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class Comment : BaseEntity
{
    private Comment(Guid id,
        string text,
        Guid taskId,
        Guid memberId) 
        : base(id)
    {
        Text = text;
        TaskId = taskId;
        MemberId = memberId;
    }

    public string Text { get; private set; }
    
    public DateTimeOffset CreatedOn { get; private set; }

    public DateTimeOffset? LastUpdatedOn { get; private set; }

    public Guid TaskId { get; private set; }
    
    public Guid MemberId { get; private set; }

    public Task Task { get; private set; }

    public Member Member { get; private set; }

    public static Comment Create(string text, Guid taskId, Guid memberId, IDateTimeOffsetProvider dateTimeOffsetProvider)
    {
        var comment = new Comment(
            Guid.NewGuid(),
            text,
            taskId,
            memberId);

        comment.CreatedOn = dateTimeOffsetProvider.UtcNow;

        return comment;
    }
}
