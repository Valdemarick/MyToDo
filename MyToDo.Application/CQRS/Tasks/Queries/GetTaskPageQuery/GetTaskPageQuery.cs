using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Application.Common.Dtos.Tasks;

namespace MyToDo.Application.CQRS.Tasks.Queries.GetTaskPageQuery;

public sealed record GetTaskPageQuery(TaskPageRequestDto Parameters) : IQuery<TaskPagedListDto>;
