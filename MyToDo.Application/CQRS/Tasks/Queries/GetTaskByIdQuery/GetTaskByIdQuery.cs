using MyToDo.Application.Abstractions.Messaging;
using MyToDo.HttpContracts.Tasks;

namespace MyToDo.Application.CQRS.Tasks.Queries.GetTaskByIdQuery;

public record GetTaskByIdQuery(Guid Id) : IQuery<TaskShortInfoDto>;
