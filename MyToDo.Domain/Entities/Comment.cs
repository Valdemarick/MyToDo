using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class Comment : BaseEntity
{
    private readonly List<Member> _members = new();

    private Comment(Guid id,
        string text,
        Guid taskId,
        Member member)
        : base(id)
    {
        Text = text;
        TaskId = taskId;

        _members.Add(member);
    }

    protected Comment()
    {
    }

    public string Text { get; private set; }
    
    public DateTimeOffset CreatedOn { get; private set; }

    public DateTimeOffset? LastUpdatedOn { get; private set; }

    public Guid TaskId { get; private set; }

    public Task Task { get; private set; }

    public IReadOnlyCollection<Member> Members => _members;

    public static Comment Create(string text, Guid taskId, Member member, IDateTimeOffsetProvider dateTimeOffsetProvider)
    {
        var comment = new Comment(
            Guid.NewGuid(),
            text,
            taskId,
            member);

        comment.CreatedOn = dateTimeOffsetProvider.UtcNow;

        return comment;
    }

    public void UpdateText(string updatedText, IDateTimeOffsetProvider dateTimeOffsetProvider)
    {
        Text = updatedText;

        LastUpdatedOn = dateTimeOffsetProvider.UtcNow;
    }
}
