namespace MyToDo.Application.Common.Dtos.Tasks;

public sealed record TaskShortInfoDto(
    Guid Id,
    string Title,
    string CreatorName,
    DateTimeOffset CreatedOn,
    DateTimeOffset Deadline);
    