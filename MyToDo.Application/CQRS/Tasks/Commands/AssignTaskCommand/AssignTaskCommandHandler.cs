using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Factories;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Factories;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Tasks.Commands.AssignTaskCommand;

internal sealed class AssignTaskCommandHandler : ICommandHandler<AssignTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskExecutorFactory _taskExecutorFactory;

    public AssignTaskCommandHandler(
        ITaskRepository taskRepository, 
        IMemberRepository memberRepository,
        IUnitOfWork unitOfWork,
        ITaskExecutorFactory taskExecutorFactory)
    {
        _taskRepository = taskRepository;
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
        _taskExecutorFactory = taskExecutorFactory;
    }

    public async Task<Result> Handle(AssignTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId, cancellationToken, isTracking: true);
        if (task is null)
        {
            return Result.Failure(DomainErrors.Task.TaskNotFound);
        }

        var member = await _memberRepository.GetByIdWithoutTrackingAsync(request.MemberId, cancellationToken);
        if (member is null)
        {
            return Result.Failure(DomainErrors.Member.MemberNotFound);
        }

        var createTaskExecutorResult = _taskExecutorFactory.Create(member.FullName, request.MemberId);
        if (createTaskExecutorResult.IsFailure)
        {
            return Result.Failure(createTaskExecutorResult.Error);
        }
        
        task.Assign(createTaskExecutorResult.Value);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
