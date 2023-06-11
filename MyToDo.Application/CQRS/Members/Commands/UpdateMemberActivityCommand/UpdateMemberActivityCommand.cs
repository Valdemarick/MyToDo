using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Members.Commands.UpdateMemberActivityCommand;

public sealed record UpdateMemberActivityCommand(Guid MemberId, bool IsActive) : ICommand;
