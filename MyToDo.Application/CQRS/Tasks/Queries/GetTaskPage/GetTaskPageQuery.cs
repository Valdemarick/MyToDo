using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Application.Common.Dtos.Tasks;

namespace MyToDo.Application.CQRS.Tasks.Queries.GetTaskPage;

public sealed record GetTaskPageQuery(int PageNumber = 1, int PageSize = 10) : IQuery<List<PagedTaskDto>>;
