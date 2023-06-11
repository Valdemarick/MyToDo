namespace MyToDo.HttpContracts.Tasks;

public sealed record TaskShortInfoDto(
    Guid Id,
    string Title,
    string CreatorName,
    DateTimeOffset CreatedOn,
    DateTimeOffset Deadline);
    