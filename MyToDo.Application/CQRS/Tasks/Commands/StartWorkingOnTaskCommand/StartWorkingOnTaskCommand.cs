using ICommand = MyToDo.Application.Abstractions.Messaging.ICommand;

namespace MyToDo.Application.CQRS.Tasks.Commands.StartWorkingOnTaskCommand;

public sealed record StartWorkingOnTaskCommand(Guid Id) : ICommand;
