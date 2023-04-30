using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Members.Commands.LoginCommand;

public sealed record LoginCommand(
    string Email,
    string Password) : ICommand<string>;
    