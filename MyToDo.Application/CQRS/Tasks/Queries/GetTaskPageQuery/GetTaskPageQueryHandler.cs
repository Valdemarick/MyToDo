using AutoMapper;
using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Application.Common.Dtos.Tasks;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Shared;
using MyToDo.Domain.ValueObjects.Requests;

namespace MyToDo.Application.CQRS.Tasks.Queries.GetTaskPageQuery;

internal sealed class GetTaskPageQueryHandler : IQueryHandler<GetTaskPageQuery, TaskPagedListDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public GetTaskPageQueryHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<Result<TaskPagedListDto>> Handle(GetTaskPageQuery query, CancellationToken cancellationToken)
    {
        var request = _mapper.Map<TaskPageRequest>(query.Parameters);
        
        var taskPagedList = await _taskRepository.GetPageAsync(request, cancellationToken);

        var tasksDto = _mapper.Map<List<TaskShortInfoDto>>(taskPagedList.Items);

        var taskPagedListDto = new TaskPagedListDto(tasksDto, taskPagedList.TotalCount,
            query.Parameters.PageIndex, query.Parameters.PageSize);

        return Result.Success(taskPagedListDto);
    }
}
