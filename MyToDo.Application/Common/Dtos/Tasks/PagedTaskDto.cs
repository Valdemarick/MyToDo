namespace MyToDo.Application.Common.Dtos.Tasks;

public sealed record PagedTaskDto(
    Guid Id,
    string Title,
    string CreatorName, 
    DateTimeOffset CreatedOn,
    DateTimeOffset? LastUpdatedOn);
    