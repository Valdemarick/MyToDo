namespace MyToDo.Application.Common.Dtos.Tasks;

public sealed record class PagedTaskDto(
    Guid Id,
    string Name,
    string CreatorName,
    DateTimeOffset CreatedOn,
    DateTimeOffset? LastUpdateOn);
    