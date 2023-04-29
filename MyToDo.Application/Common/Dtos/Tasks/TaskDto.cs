namespace MyToDo.Application.Common.Dtos.Tasks;

public sealed class TaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CreatorName { get; set; }
    public string? ExecutorName { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? LastUpdatedOn { get; set; }
}
