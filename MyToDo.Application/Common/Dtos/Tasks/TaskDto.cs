namespace MyToDo.Application.Common.Dtos.Tasks;

public sealed record TaskDto(
    Guid Id,
    string Title,
    string Description,
    string CreatorName,
    string? ExecutorName,
    DateTimeOffset CreatedOn,
    DateTimeOffset? LastUpdatedOn);
