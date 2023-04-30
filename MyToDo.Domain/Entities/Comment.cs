using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class Comment : BaseEntity
{
    private Comment(string text,
        Guid taskId,
        Guid writerId)
        : base(Guid.NewGuid())
    {
        Text = text;
        TaskId = taskId;
    }

    protected Comment()
    {
    }

    public string Text { get; private set; }
    
    public DateTimeOffset CreatedOn { get; private set; }

    public DateTimeOffset? LastUpdatedOn { get; private set; }

    public Guid TaskId { get; private set; }
    
    public Guid WriterId { get; private set; }

    public Task Task { get; private set; }

    public void UpdateText(string updatedText)
    {
        Text = updatedText;
    }

    public void SetCreatedOn(DateTimeOffset dateTimeOffset)
    {
        CreatedOn = dateTimeOffset;
    }

    public void SetLastUpdatedOn(DateTimeOffset dateTimeOffset)
    {
        LastUpdatedOn = dateTimeOffset;
    }
    
    internal static Comment Create(string text, Guid taskId, Guid writerId)
    {
        var comment = new Comment(
            text,
            taskId,
            writerId);

        return comment;
    }
}
