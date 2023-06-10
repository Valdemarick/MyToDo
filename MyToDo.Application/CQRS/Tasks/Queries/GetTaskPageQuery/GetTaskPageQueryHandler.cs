using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Enums;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using MyToDo.Domain.ValueObjects.Requests;
using MyToDo.HttpContracts.Tasks;
using TaskStatus = MyToDo.Domain.Enums.TaskStatus;

namespace MyToDo.Application.CQRS.Tasks.Queries.GetTaskPageQuery;

internal sealed class GetTaskPageQueryHandler : IQueryHandler<GetTaskPageQuery, TaskPagedListDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetTaskPageQueryHandler(ITaskRepository taskRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<TaskPagedListDto>> Handle(GetTaskPageQuery query, CancellationToken cancellationToken)
    {
        Guid authenticatedMemberId = default;
        if (query.IsShowOnlyMyTasks)
        {
            var authenticatedMemberIdString =
                _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(authenticatedMemberIdString, out authenticatedMemberId))
            {
                return Result.Failure(DomainErrors.FailedToParseId);
            }
        }
        
        var request = new TaskPageRequest(
            query.SearchString, 
            query.PageIndex, 
            query.PageSize,
            (TaskStatus)query.TaskStatus,
            (TaskType)query.TaskType,
            (Priority)query.Priority,
            authenticatedMemberId == default ? null : authenticatedMemberId);
        
        var taskPagedList = await _taskRepository.GetPageAsync(request, cancellationToken);

        var tasksDto = _mapper.Map<List<TaskShortInfoDto>>(taskPagedList.Items);

        var taskPagedListDto = new TaskPagedListDto(tasksDto, taskPagedList.TotalCount,
            query.PageIndex, query.PageSize);

        return Result.Success(taskPagedListDto);
    }
}
