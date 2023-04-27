using AutoMapper;
using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Application.Common.Dtos.Tasks;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Tasks.Queries.GetTaskPage;

internal sealed class GetTaskPageQueryHandler : IQueryHandler<GetTaskPageQuery, List<PagedTaskDto>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public GetTaskPageQueryHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<PagedTaskDto>>> Handle(GetTaskPageQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetPageAsync(
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        var tasksDto = _mapper.Map<List<PagedTaskDto>>(tasks);

        return Result.Success(tasksDto);
    }
}
