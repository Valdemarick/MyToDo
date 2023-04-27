using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Application.Common.Dtos.Tasks;

namespace MyToDo.Application.CQRS.Tasks.Queries.GetTaskById;

public record GetTaskByIdQuery(Guid Id) : IQuery<TaskDto>;
