using MyToDo.Application.Abstractions.Messaging;
using MyToDo.HttpContracts.Tasks;

namespace MyToDo.Application.CQRS.Tasks.Queries.GetTaskPageQuery;

public sealed record GetTaskPageQuery(
    string? SearchString,
    int PageIndex,
    int PageSize) : IQuery<TaskPagedListDto>;
