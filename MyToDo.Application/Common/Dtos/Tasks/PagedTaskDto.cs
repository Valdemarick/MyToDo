namespace MyToDo.Application.Common.Dtos.Tasks;

public sealed record PagedTaskDto(
    Guid Id,
    string Title,
    string CreatorName,
    string? ExecutorName,
    DateTime CreatedOn,
    DateTime? LastUpdatedOn,
    DateTime Deadline);
    