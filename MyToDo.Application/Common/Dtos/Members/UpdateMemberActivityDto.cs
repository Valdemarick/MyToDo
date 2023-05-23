namespace MyToDo.Application.Common.Dtos.Members;

public sealed record UpdateMemberActivityDto(Guid MemberId, bool IsActive);
