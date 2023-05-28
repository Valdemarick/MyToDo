using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Tasks.Commands.WriteCommentCommand;

public sealed record WriteCommentCommand(
    string Text,
    Guid TaskId,
    Guid MemberId) : ICommand;
