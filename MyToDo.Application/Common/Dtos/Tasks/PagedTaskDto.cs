namespace MyToDo.Application.Common.Dtos.Tasks;

public sealed class PagedTaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string CreatorName { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? LastUpdatedOn { get; set; }
}
    