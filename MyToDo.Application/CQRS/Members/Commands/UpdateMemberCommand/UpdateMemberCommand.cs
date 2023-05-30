using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Members.Commands.UpdateMemberCommand;

public record UpdateMemberCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    Guid RoleId,
    bool IsActive) : ICommand;
    