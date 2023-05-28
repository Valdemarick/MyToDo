using AutoMapper;
using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Tasks;

namespace MyToDo.Application.CQRS.Tasks.Queries.GetTaskByIdQuery;

internal sealed class GetTaskByIdQueryHandler : IQueryHandler<GetTaskByIdQuery, TaskShortInfoDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public GetTaskByIdQueryHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<Result<TaskShortInfoDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);
        if (task is null)
        {
            return Result.Failure(DomainErrors.Task.TaskNotFound);
        }

        var taskDto = _mapper.Map<TaskShortInfoDto>(task);

        return Result.Success(taskDto);
    }
}
