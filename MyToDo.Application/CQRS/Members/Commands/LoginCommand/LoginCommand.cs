using MyToDo.Application.Abstractions.Messaging;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Application.CQRS.Members.Commands.LoginCommand;

public sealed record LoginCommand(
    string Email,
    string Password) : ICommand<MemberSessionDto>;
    