using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Tasks.Commands.AssignToMeCommand;

internal sealed class AssignToMeCommandHandler : ICommandHandler<AssignToMeCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMemberRepository _memberRepository;

    public AssignToMeCommandHandler(
        ITaskRepository taskRepository, 
        IUnitOfWork unitOfWork, 
        IHttpContextAccessor httpContextAccessor, 
        IMemberRepository memberRepository)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _memberRepository = memberRepository;
    }

    public async Task<Result> Handle(AssignToMeCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdWithoutIncludesAsync(request.TaskId, cancellationToken, true);
        if (task is null)
        {
            return Result.Failure(DomainErrors.Task.TaskNotFound);
        }
        
        var authenticatedMemberIdString =
            _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(authenticatedMemberIdString, out var authenticatedMemberId))
        {
            return Result.Failure(DomainErrors.FailedToParseId);
        }

        var authenticatedMember = await _memberRepository.GetByIdWithoutTrackingAsync(authenticatedMemberId, cancellationToken);
        if (authenticatedMember is null)
        {
            return Result.Failure(DomainErrors.Member.MemberNotFound);
        }

        task.Assign(authenticatedMember);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
