namespace MyToDo.HttpContracts.Members;

public sealed record UpdateMemberActivityDto(Guid MemberId, bool IsActive);
