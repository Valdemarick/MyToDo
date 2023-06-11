using MyToDo.Application.Abstractions.Messaging;
using MyToDo.HttpContracts.Enums;
using MyToDo.HttpContracts.Tasks;

namespace MyToDo.Application.CQRS.Tasks.Queries.GetTaskPageQuery;

public sealed record GetTaskPageQuery(
    string? SearchString,
    int PageIndex,
    int PageSize,
    TaskStatusDto TaskStatus,
    TaskTypeDto TaskType,
    PriorityDto Priority,
    bool IsShowOnlyMyTasks,
    List<Guid> TagIds) : IQuery<TaskPagedListDto>;
