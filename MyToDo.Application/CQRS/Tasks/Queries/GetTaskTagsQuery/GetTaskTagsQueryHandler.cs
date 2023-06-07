using AutoMapper;
using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Tags;

namespace MyToDo.Application.CQRS.Tasks.Queries.GetTaskTagsQuery;

internal sealed class GetTaskTagsQueryHandler : IQueryHandler<GetTaskTagsQuery, List<TagDto>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public GetTaskTagsQueryHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<TagDto>>> Handle(GetTaskTagsQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId, cancellationToken);
        if (task is null)
        {
            return Result.Failure(DomainErrors.Task.TaskNotFound);
        }

        var tags = task.Tags.ToList();

        var listOfDto = _mapper.Map<List<TagDto>>(tags);

        return Result.Success(listOfDto);
    }
}
